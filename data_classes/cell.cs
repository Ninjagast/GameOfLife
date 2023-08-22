using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGameOfLive.data_classes
{
    public class Cell
    {
        private bool Alive    { get; set; } = false;
        private bool FutureState { get; set; } = false;
        private List<Pos> Neighbors { get; set; } = new List<Pos>();

        private Pos _pos;

        private Texture2D _texture;
        private Texture2D _emptyTexture;

        public Cell(Texture2D texture, Texture2D emptyTexture, Pos pos)
        {
            _pos = pos;
            _texture = texture;
            _emptyTexture = emptyTexture;
        }

        public void AddNeighbour(Pos cell)
        {
            Neighbors.Add(cell);

        }

        public void draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            Grid grid = Grid.Instance;

            if (Alive)
            {
                spriteBatch.Draw(_texture,      new Vector2(_pos.x * 50, _pos.y * 50), Color.White);
            }
            else
            {
                spriteBatch.Draw(_emptyTexture, new Vector2(_pos.x * 50, _pos.y * 50), Color.White);
            }
        }

        public void alive()
        {
            Alive = true;
        }

        public void tick(List<List<Cell>> gridCells)
        {
            int aliveNeigh = 0;
            foreach (var neighbor in Neighbors)
            {
                if (gridCells[neighbor.y][neighbor.x].isAlive())
                {
                    aliveNeigh++;
                }

            }

            if (Alive == false)
            {
                if (aliveNeigh == 3)
                {
                    FutureState = true;
                }
            }
            else if (aliveNeigh < 2)
            {
                FutureState = false;
            }
            else if (aliveNeigh > 3)
            {
                FutureState = false;
            }
        }

        public void switchAround()
        {
            Alive = FutureState;
        }
        
        private bool isAlive()
        {
            return Alive;
        }

        public void Toggle()
        {
            Alive = !Alive;
            FutureState = Alive;
        }
    }
}
