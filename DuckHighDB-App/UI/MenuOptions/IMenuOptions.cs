using System;
using System.Collections.Generic;
using System.Text;

namespace DuckHighDB_App.UI.MenuOptions
{
    public interface IMenuOptions
    {
        /// <summary>
        /// Title of the menu option.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Executes the menu option for the given user.
        /// </summary>
        void Execute();
    }
}
