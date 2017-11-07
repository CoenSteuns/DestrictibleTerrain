using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using General.CostumGameComponents;
using General.Input;
using General.UI;
using General;

using DestructibleTerain.Scripts;

using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace DestructibleTerain
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Example : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Level level;
        Button button;

        List<Bomb> bombs = new List<Bomb>();

        public Example()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here


            button = new Button(Content.Load<Texture2D>("retry"));
            button.Scale = new Vector2(0.2f, 0.2f);
            button.OnClick += StartGame;
            StartGame();
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (InputHelper.MouseButtonDown(MouseButtons.left))
            {
                var b = new Bomb(Content.Load<Texture2D>("weapon"), InputHelper.MouseState.Position.ToVector2());
                b.CenterOrigin();
                bombs.Add(b);
            }

            if (InputHelper.GetKeyDown(Keys.Space))
            {
                for (int i = 0; i < bombs.Count; i++)
                {
                    bombs[i].Explode(level, Content.Load<Texture2D>("explosion"));
                }

                bombs = new List<Bomb>();
            }

            ComponentManegment.Instance.UpdateComponents(gameTime);
            InputHelper.Update();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            ComponentManegment.Instance.DrawComponents(gameTime, spriteBatch);
            base.Draw(gameTime);
        }

        uint[] original;


        public void StartGame()
        {
            Texture2D t = Content.Load<Texture2D>("level");
            if (original == null)
            {
                original = new uint[t.Width * t.Height];
                t.GetData(original, 0, t.Width * t.Height);
            }
            t.SetData(original);

            level = new Level(t);
            if(bombs != null)
            {
                for (int i = 0; i < bombs.Count; i++)
                {
                    ComponentManegment.Instance.Dispose(bombs[i]);
                }
            }
            bombs = new List<Bomb>();

        }
    }
}