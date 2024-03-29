﻿using System;
using System.Windows.Input;

namespace BarcoPVG.ViewModels
{
    internal class Command : ICommand
    {
        private Action<object>? _action;
        public Command(Action<object>? action)
        {
            _action = action;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            //throw new NotImplementedException();
            //zal niet altijd true zijn, kan met if-else opgevangen worden
            //adhv de parameter parameter die meegegeven wordt
            return true;
        }

        public void Execute(object? parameter)
        {
            //throw new NotImplementedException();
            if (parameter != null)
            {
                //actie uitgevoerd worden met de variabele 'parameter'
                _action(parameter);
            }
            else
            {
                //actie met foutmelding
                _action("foutmelding");
            }
        }
    }
}