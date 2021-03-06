﻿using DAN_LI_Milos_Peric.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DAN_LI_Milos_Peric.View
{
    /// <summary>
    /// Interaction logic for DoctorView.xaml
    /// </summary>
    public partial class DoctorView : Window
    {
        public DoctorView(tblDoctor doctorUser)
        {
            InitializeComponent();
            DataContext = new DoctorViewModel(this, doctorUser);
            tbDoctor.Text = "Doctor: " + doctorUser.UserName;
        }
    }
}
