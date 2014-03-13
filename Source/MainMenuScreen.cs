using MenuBuddy;
using Microsoft.Xna.Framework;

namespace InsertCoinBuddySample
{
	/// <summary>
	/// The main menu screen is the first thing displayed when the game starts up.
	/// </summary>
	internal class MainMenuScreen : MenuScreen, IMainMenu
	{
		#region Initialization

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public MainMenuScreen() : base("Main Menu")
		{
			//add empty menu option so it don't crash
			var exitMenuEntry = new MenuEntry("");
			exitMenuEntry.Selected += OnCancel;
			MenuEntries.Add(exitMenuEntry);
		}

		#endregion //Initialization

		#region Methods

		public override void LoadContent()
		{
		}

		#endregion //Methods

		#region Handle Input

		/// <summary>
		/// Ignore the cancel message from the main menu
		/// </summary>
		protected override void OnCancel(PlayerIndex playerIndex)
		{
			//do nothing here!
		}

		#endregion
	}
}