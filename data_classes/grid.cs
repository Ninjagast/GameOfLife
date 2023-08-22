using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheGameOfLive.data_classes
{
    public sealed class Grid
    {
        private static Grid instance = null;
        private static readonly object padlock = new object();

        public List<List<Cell>> GridCells = new List<List<Cell>>();

        public static Grid Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new Grid();
                    }
                    return instance;
                }
            }
        }

        public void FillGrid(Pos size, Texture2D texture, Texture2D emptyTexture)
        {
            for (int y = 0; y < size.y; y++)
            {
                GridCells.Add(new List<Cell>());
                for (int x = 0; x < size.x; x++)
                {
                    Cell insertCell = new Cell(texture, emptyTexture, new Pos(x ,y));

//                  we are at the top row
                    if (y == 0)
                    {
//                      most left cell
                        if (x == 0)
                        {
                            insertCell.AddNeighbour(new Pos(x + 1, y));
                            insertCell.AddNeighbour(new Pos(x + 1, y + 1));
                            insertCell.AddNeighbour(new Pos(x, y + 1));
                        }
//                      most right cell
                        else if(x == size.x - 1)
                        {
                            insertCell.AddNeighbour(new Pos(x - 1, y));
                            insertCell.AddNeighbour(new Pos(x - 1, y + 1));
                            insertCell.AddNeighbour(new Pos(x, y + 1));
                        }
//                      somewhere in the middle
                        else
                        {
                            insertCell.AddNeighbour(new Pos(x - 1, y));
                            insertCell.AddNeighbour(new Pos(x + 1, y));
                            insertCell.AddNeighbour(new Pos(x - 1, y + 1));
                            insertCell.AddNeighbour(new Pos(x + 1, y + 1));
                            insertCell.AddNeighbour(new Pos(x, y + 1));
                        }
                    }
//                  we are at the bottom row
                    else if(y == size.y - 1)
                    {
//                      most left cell
                        if (x == 0)
                        {
                            insertCell.AddNeighbour(new Pos(x + 1, y));
                            insertCell.AddNeighbour(new Pos(x + 1, y - 1));
                            insertCell.AddNeighbour(new Pos(x, y - 1));
                        }
//                      most right cell
                        else if (x == size.x - 1)
                        {
                            insertCell.AddNeighbour(new Pos(x - 1, y));
                            insertCell.AddNeighbour(new Pos(x - 1, y - 1));
                            insertCell.AddNeighbour(new Pos(x, y - 1));
                        }
//                      somewhere in the middle
                        else
                        {
                            insertCell.AddNeighbour(new Pos(x - 1, y));
                            insertCell.AddNeighbour(new Pos(x + 1, y));
                            insertCell.AddNeighbour(new Pos(x - 1, y - 1));
                            insertCell.AddNeighbour(new Pos(x + 1, y - 1));
                            insertCell.AddNeighbour(new Pos(x, y - 1));
                        }
                    }
//                  we are at the middle somewhere
                    else
                    {
//                      most left cell
                        if (x == 0)
                        {
                            insertCell.AddNeighbour(new Pos(x + 1, y));
                            insertCell.AddNeighbour(new Pos(x + 1, y - 1));
                            insertCell.AddNeighbour(new Pos(x + 1, y + 1));
                            insertCell.AddNeighbour(new Pos(x, y - 1));
                            insertCell.AddNeighbour(new Pos(x, y + 1));
                        }
//                      most right cell
                        else if (x == size.x - 1)
                        {
                            insertCell.AddNeighbour(new Pos(x - 1, y));
                            insertCell.AddNeighbour(new Pos(x - 1, y - 1));
                            insertCell.AddNeighbour(new Pos(x - 1, y + 1));
                            insertCell.AddNeighbour(new Pos(x, y - 1));
                            insertCell.AddNeighbour(new Pos(x, y + 1));
                        }
//                      somewhere in the middle
                        else
                        {
                            insertCell.AddNeighbour(new Pos(x - 1, y));
                            insertCell.AddNeighbour(new Pos(x - 1, y - 1));
                            insertCell.AddNeighbour(new Pos(x - 1, y + 1));
                            insertCell.AddNeighbour(new Pos(x + 1, y));
                            insertCell.AddNeighbour(new Pos(x + 1, y - 1));
                            insertCell.AddNeighbour(new Pos(x + 1, y + 1));
                            insertCell.AddNeighbour(new Pos(x, y - 1));
                            insertCell.AddNeighbour(new Pos(x, y + 1));
                        }
                    }

                    GridCells[y].Add(insertCell);
                }
            }
        }

        public void draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            foreach (var row in GridCells)
            {
                foreach (var cell in row)
                {
                    cell.draw(graphicsDevice, spriteBatch);
                }
            }
        }

        public void tick()
        {
            foreach (var row in GridCells)
            {
                foreach (var cell in row)
                {
                    cell.tick(GridCells);
                }
            }
            foreach (var row in GridCells)
            {
                foreach (var cell in row)
                {
                    cell.switchAround();
                }
            }
        }
    }
}
