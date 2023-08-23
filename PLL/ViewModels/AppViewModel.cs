using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Linq;
using DAL.Models;
using DAL;

namespace PhotoStudio.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private DbDataOperation DbContext;

        public ApplicationViewModel()
        {
            DbContext = new DbDataOperation();
        }

        private RelayCommand closeCommand;
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??
                  (closeCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          Application.Current.Shutdown();
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }

        private RelayCommand newGuestCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand NewGuestCommand
        {
            get
            {
                return newGuestCommand ??
                  (newGuestCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          Guest newGuest = new Guest();
                          newGuest.Name = "11";
                          newGuest.Surname = "222";
                          newGuest.Passport = "56456";
                          newGuest.BirthDate = new DateTime();
                          newGuest.Gender = false;

                          var guest = DbContext.FindGuest(newGuest);
                      }
                      catch (Exception ex)
                      {
                          MessageBox.Show(ex.Message);
                      }
                  }));
            }
        }
    }
}
