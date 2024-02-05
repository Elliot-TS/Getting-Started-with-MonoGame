using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GettingStarted;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    Texture2D squirrelTexture;
    RotatedRect squirrelRect;
    Vector2 squirrelVelocity;
    float angVel;

    bool xBounceLock = false;
    bool yBounceLock = false;

    Random rnd;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        rnd = new Random();

        squirrelRect = new RotatedRect(
            _graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2,
            160, 120, (float)rnd.NextDouble() * 3.14f
        );

        squirrelVelocity = new Vector2(((float)rnd.NextDouble()-0.5f) * 6, ((float)rnd.NextDouble()-0.5f) * 6);
        angVel = (float)rnd.NextDouble() * 0.02f;

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        squirrelTexture = Content.Load<Texture2D>("squirrel");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        squirrelRect.move(squirrelVelocity);
        squirrelRect.rotate(angVel);
        
        if (squirrelRect.getBoundingRight() > _graphics.PreferredBackBufferWidth || squirrelRect.getBoundingLeft() < 0)
        {
            if (!xBounceLock) {
                squirrelVelocity.X *= -1f;
                angVel *= -1f;
            }
            xBounceLock = true;
        }
        else {
            xBounceLock = false;
        }
        if (squirrelRect.getBoundingBottom() > _graphics.PreferredBackBufferHeight || squirrelRect.getBoundingTop() < 0)
        {
            if (!yBounceLock)
            {
                squirrelVelocity.Y *= -1f;
                angVel *= -1f;
            }
            yBounceLock = true;
        }
        else {
            yBounceLock = false;
        }


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();
        _spriteBatch.Draw(
            squirrelTexture, 
            squirrelRect.getPosition(), 
            null,
            Color.White,
            squirrelRect.getAngle(),
            new Vector2(squirrelTexture.Width/2, squirrelTexture.Height/2),
            new Vector2(0.1f,0.1f),
            SpriteEffects.None,
            0f
        );
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
