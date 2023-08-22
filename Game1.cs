using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheGameOfLive.data_classes;

namespace TheGameOfLive
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Grid _grid = null;
        private readonly Pos _gridSize = new Pos(100, 100);
        private bool _pause = true;
        private KeyboardState _lastKeyboardState;
        private MouseState _lastMouseState;
        private float _scale = 0.5f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d); //60);
        }

        protected override void Initialize()
        {
            _grid = Grid.Instance;
            _graphics.PreferredBackBufferWidth = 500;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = 500;   // set this value to the desired height of your window
            _graphics.ApplyChanges();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D CellTexture = Content.Load<Texture2D>("tile");
            Texture2D EmptyCellTexture = Content.Load<Texture2D>("empty");

            _grid.FillGrid(_gridSize, CellTexture, EmptyCellTexture);
        }

        protected override void Update(GameTime gameTime)
        {
            LookAtKeyboard();
            
            if (!_pause)
            {
                _grid.tick();
                TargetElapsedTime = TimeSpan.FromSeconds(1d / 3d);
            }
            else
            {
                TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);
            }
            
            base.Update(gameTime);
        }

        private void LookAtKeyboard()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyUp(Keys.Space) && _lastKeyboardState.IsKeyDown(Keys.Space))
            {
                _pause = !_pause;
            }
            
            if (Keyboard.GetState().IsKeyUp(Keys.NumPad1) && _lastKeyboardState.IsKeyDown(Keys.NumPad1))
            {
                _scale = 0.5f;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.NumPad2) && _lastKeyboardState.IsKeyDown(Keys.NumPad2))
            {
                _scale = 0.3f;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.NumPad3) && _lastKeyboardState.IsKeyDown(Keys.NumPad3))
            {
                _scale = 0.1f;
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && _lastMouseState.LeftButton == ButtonState.Released && _pause)
            {
                int cellX = (int)(Mouse.GetState().Position.X / (_scale * 50));
                int cellY = (int)(Mouse.GetState().Position.Y / (_scale * 50));
                
                Console.WriteLine(cellX);
                Console.WriteLine(cellY);

                _grid.GridCells[cellY][cellX].Toggle();
            }

            _lastMouseState = Mouse.GetState();
            _lastKeyboardState = Keyboard.GetState();
        }
        
        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(_scale));

            GraphicsDevice.Clear(Color.Black);

                _grid.draw(GraphicsDevice, _spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);

        }
    }
}
