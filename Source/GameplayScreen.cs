using FontBuddyLib;
using InsertCoinBuddy;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ResolutionBuddy;

namespace OperatorSettingsExample
{
	/// <summary>
	/// This screen displays on top of all the other screens
	/// </summary>
	internal class GameplayScreen : GameScreen
	{
		#region Fields

		const float TextVelocity = 3.0f;

		/// <summary>
		/// current location of the text
		/// </summary>
		Vector2 TextLocation = Vector2.Zero;

		/// <summary>
		/// current direction the text is travelling in
		/// </summary>
		Vector2 TextDirection;

		/// <summary>
		/// thing for writing text
		/// </summary>
		FontBuddy Text;

		CreditsManager _manager;

		#endregion //Fields

		#region Initialization

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public GameplayScreen(CreditsManager manager)
		{
			TextLocation = new Vector2(Resolution.TitleSafeArea.Center.X, Resolution.TitleSafeArea.Center.Y);
			_manager = manager;
			TextDirection = new Vector2(TextVelocity, TextVelocity);
			Text = new FontBuddy();
		}

		#endregion //Initialization

		#region Methods

		public override void LoadContent()
		{
			Text.Font = ScreenManager.Game.Content.Load<SpriteFont>("ArialBlack72");
		}

		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

			//does the uesr want to exit?
			if (Keyboard.GetState().IsKeyDown(Keys.Space))
			{
				//Load the main menu back up
				LoadingScreen.Load(ScreenManager, false, null, ScreenManager.GetMainMenuScreenStack());

				//the game isn't playing anymore
				_manager.GameInPlay = false;
			}

			//move the text
			TextLocation += TextDirection;

			//bounce the text off the walls
			if ((TextLocation.X - 256) <= 0)
			{
				TextDirection.X = TextVelocity;
			}
			else if ((TextLocation.X + 256) >= Resolution.ScreenArea.Right)
			{
				TextDirection.X = -TextVelocity;
			}

			if (TextLocation.Y <= 0)
			{
				TextDirection.Y = TextVelocity;
			}
			else if ((TextLocation.Y + 128) >= Resolution.ScreenArea.Bottom)
			{
				TextDirection.Y = -TextVelocity;
			}
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			//draw the text
			ScreenManager.SpriteBatchBegin();
			Text.Write("Gameplay Screen!!!", TextLocation, Justify.Center, 1.0f, Color.Red, ScreenManager.SpriteBatch, 0.0f);

			Vector2 quitLocation = new Vector2(Resolution.TitleSafeArea.Center.X, Resolution.TitleSafeArea.Top);

			Text.Write("Press 'space' to end game", quitLocation, Justify.Center, 0.75f, Color.Green, ScreenManager.SpriteBatch, 0.0f);

			ScreenManager.SpriteBatchEnd();
		}

		#endregion
	}
}