﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WooFractal
{
    /// <summary>
    /// Interaction logic for RenderControls.xaml
    /// </summary>
    public partial class RenderControls : UserControl, IGUIUpdateable
    {

        RenderOptions _Parent;
        public RenderControls(RenderOptions parent)
        {
            InitializeComponent();

            _Parent = parent;

            RenderSliders();

            PopulateCombos();
        }

        private void PopulateCombos()
        {
            // background
            foreach (Script script in _Parent._Backgrounds)
            {
                comboBox1.Items.Add(script._Name);
            }

            // lighting environments
            foreach (Script script in _Parent._LightingEnvironments)
            {
                comboBox2.Items.Add(script._Name);
            }

            // DE modes
            comboBox3.Items.Add("Standard Mandelbulb");
            comboBox3.Items.Add("KIFS / Mandelbox");
            comboBox3.Items.Add("Last iteration");
            comboBox3.Items.Add("4-point Buddhi/Makin");

            // minimum distance mode
            comboBox4.Items.Add("Fixed");
            comboBox4.Items.Add("Screenspace");

            comboBox1.SelectedIndex = _Parent._Background;
            comboBox2.SelectedIndex = _Parent._Lighting;
            comboBox3.SelectedIndex = _Parent._DEMode;
            comboBox4.SelectedIndex = _Parent._DistanceMinimumMode;
        }

        public void RenderSliders()
        {
            wooSlider1.Set(_Parent._DistanceMinimum, 0, 5, this);
            wooSlider2.Set(_Parent._DistanceIterations, 1, 1024, this);
            wooSlider3.Set(_Parent._StepSize, 0.01, 1.0, this);
            wooSlider4.Set(_Parent._DistanceExtents, 0.5, 10.0, this);
            wooSlider5.Set(_Parent._FractalIterationCount, 1, 250, this);
            wooSlider6.Set(_Parent._ColourIterationCount, 1, 250, this);
            wooSlider7.Set(_Parent._HeadLightStrength, 0, 2, this);
            wooSlider8.Set(_Parent._SunHeight, 0, 90, this);
            wooSlider9.Set(_Parent._SunDirection, 0, 360, this);
            checkBox1.IsChecked = _Parent._Headlight;
        }

        public void GUIUpdate()
        {
            RenderOptions old = (RenderOptions) _Parent.GetClone();
            _Parent._DistanceMinimum = wooSlider1.GetSliderValue();
            _Parent._DistanceIterations = wooSlider2.GetSliderValue();
            _Parent._StepSize = wooSlider3.GetSliderValue();
            _Parent._DistanceExtents = wooSlider4.GetSliderValue();
            _Parent._FractalIterationCount = (int)(wooSlider5.GetSliderValue() + 0.5);
            _Parent._ColourIterationCount = (int)(wooSlider6.GetSliderValue() + 0.5);
            _Parent._HeadLightStrength = wooSlider7.GetSliderValue();
            _Parent._SunHeight = wooSlider8.GetSliderValue();
            _Parent._SunDirection = wooSlider9.GetSliderValue();
            _Parent._Headlight = checkBox1.IsChecked.HasValue ? checkBox1.IsChecked.Value : false;

            if (_Parent._DistanceMinimum != old._DistanceMinimum
                || _Parent._DistanceIterations != old._DistanceIterations
                || _Parent._StepSize != old._StepSize
                || _Parent._DistanceExtents != old._DistanceExtents
                || _Parent._FractalIterationCount != old._FractalIterationCount
                || _Parent._ColourIterationCount != old._ColourIterationCount
                || _Parent._HeadLightStrength != old._HeadLightStrength
                || _Parent._Headlight != old._Headlight)
                ((MainWindow)System.Windows.Application.Current.MainWindow).SetDirty();
            else
                ((MainWindow)System.Windows.Application.Current.MainWindow).SetCameraDirty();
        }

        private void comboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _Parent._Background = comboBox1.SelectedIndex;
            ((MainWindow)System.Windows.Application.Current.MainWindow).SetDirty();
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _Parent._Lighting = comboBox2.SelectedIndex;
            ((MainWindow)System.Windows.Application.Current.MainWindow).SetDirty();
        }

        private void comboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _Parent._DEMode = comboBox3.SelectedIndex;
            ((MainWindow)System.Windows.Application.Current.MainWindow).SetDirty();
        }

        private void checkBox1_Modified(object sender, RoutedEventArgs e)
        {
            GUIUpdate();
        }

        private void comboBox4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _Parent._DistanceMinimumMode = comboBox4.SelectedIndex;
            ((MainWindow)System.Windows.Application.Current.MainWindow).SetDirty();
        }
    }
}
