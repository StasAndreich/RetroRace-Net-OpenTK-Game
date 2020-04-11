﻿using System;
using System.Windows.Forms;
using Racing.Objects;
using Racing.Prizes;
using RGEngine;


namespace GameLauncher
{
    public partial class LaunchWindow : Form
    {
        public LaunchWindow()
        {
            InitializeComponent();
        }


        private void StartButton_Click(object sender, EventArgs e)
        {
            using (var racingGame = new EngineCore(true))
            {
                EngineCore.AddGameObject(new Racing.Objects.Environment(@"C:\Users\smedy\Source\Repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\Environment\track.png"));
                EngineCore.AddGameObject(new DefaultCar(@"C:\Users\smedy\source\repos\OOP_CourseProject_StasMedyancev_NET_WinForms_OpenGL\RacingGame2D\Racing\Contents\Cars\lambo.png"));
                EngineCore.AddGameObject(new FuelPrize());

                racingGame.WindowState = OpenTK.WindowState.Maximized;
                racingGame.Run();
            }
        }

        private void LaunchWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
