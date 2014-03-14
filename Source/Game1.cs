using InsertCoinBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ResolutionBuddy;
using OperatorSettingsBuddy;

namespace OperatorSettingsExample
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		/// <summary>
		/// The credit manager.
		/// </summary>
		private CreditsManager _creditManager;

		private readonly DummyScreenManager _ScreenManager;

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
			Resolution.Init(ref graphics);
			Content.RootDirectory = "Content";

			//Setup the credits manager.
			_creditManager = new CreditsManager(this);
			_creditManager.CoinsPerCredit = 3; //$.75 per game
			_creditManager.FreePlay = false;
			Components.Add(_creditManager);

			// Create the screen manager component.
			_ScreenManager = new DummyScreenManager(this);
			_ScreenManager.ClearColor = new Color(0.5f, 0.5f, 0.5f);
			Components.Add(_ScreenManager);

			// Activate the first screens.
			_ScreenManager.AddScreen(_ScreenManager.GetMainMenuScreenStack(), null);
			_ScreenManager.SetTopScreen(new InsertCoinScreen("ArialBlack24", "ArialBlack24", _creditManager), null);

			//add the operator settings thing
			SettingsComponent<SettingsScreen> buddy = new SettingsComponent<SettingsScreen>(this, _ScreenManager, "OperatorSettingsExample", Keys.D3);
			Components.Add(buddy);
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			// Change Virtual Resolution
			Resolution.SetDesiredResolution(1280, 720);

			//set the desired resolution
			Resolution.SetScreenResolution(640, 480, true);

			// TODO: Add your initialization logic here
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

			//TODO: use this.Content to load your game content here 
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
				Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			//Listen for game start...
			if (Keyboard.GetState().IsKeyDown(Keys.W))
			{
				//Can we start a game?
				if (_creditManager.StartGame())
				{
					//Clear out all the screens and start a game.
					MenuBuddy.LoadingScreen.Load(_ScreenManager, false, null, new GameplayScreen(_creditManager));
					_creditManager.GameInPlay = true;
				}
			}

			// TODO: Add your update logic here
			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// Clear to Black
			graphics.GraphicsDevice.Clear(Color.Black);

			// Calculate Proper Viewport according to Aspect Ratio
			Resolution.ResetViewport();

			// The real drawing happens inside the screen manager component.
			base.Draw(gameTime);
		}
	}
}

