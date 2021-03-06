﻿using Autodesk.AdvanceSteel.Runtime;
using Dynamo.Applications.Models;
using Dynamo.Controls;
using Dynamo.ViewModels;
using DynamoShapeManager;
using System;
using MessageBox = System.Windows.Forms.MessageBox;

[assembly: CommandClassAttribute(typeof(Dynamo.Applications.CommandClass))]

namespace Dynamo.Applications
{
  /// <summary>
  /// Class that contains the definition for the command that is exposed in AutoCAD
  /// </summary>
  public class CommandClass
  {
    private static DynamoViewModel dynamoViewModel;
    private static AdvanceSteelModel advanceSteelModel;
    private static string GeometryFactoryPath = "";

    [CommandMethodAttribute("TEST_GROUP", "Create", "RunDynamo", CommandFlags.Modal | CommandFlags.UsePickSet | CommandFlags.Redraw)]
    public void Create()
    {
      try
      {
        //disable document switch while dynamo is open
        //Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.DocumentActivationEnabled = false;

        InitializeCore();

        advanceSteelModel = InitializeCoreModel();
        dynamoViewModel = InitializeCoreViewModel(advanceSteelModel);

        //show dynamo window
        Autodesk.AutoCAD.ApplicationServices.Application.ShowModelessWindow(InitializeCoreView());

        //disable Ribbon button
        DynamoASUtils.modifyRibbon(DynamoASUtils.tabUIDDynamoAS, DynamoASUtils.panelUIDDynamoAS, DynamoASUtils.buttonUIDDynamoAS, false);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.ToString());
      }
    }

    private static AdvanceSteelModel InitializeCoreModel()
    {
      string corePath = DynamoAdvanceSteelApplication.DynamoCorePath;
      return AdvanceSteelModel.Start(
          new Dynamo.Models.DynamoModel.DefaultStartConfiguration()
          {
            GeometryFactoryPath = GeometryFactoryPath,
                  //Preferences = prefs,
                  DynamoCorePath = corePath,
            SchedulerThread = new SchedulerThread(),
            PathResolver = new AdvanceSteelPathResolver()
          });
    }

    private static DynamoViewModel InitializeCoreViewModel(AdvanceSteelModel advanceSteelModel)
    {
      var viewModel = DynamoViewModel.Start(
          new DynamoViewModel.StartConfiguration()
          {
            DynamoModel = advanceSteelModel
          });
      return viewModel;
    }

    private static DynamoView InitializeCoreView()
    {
      IntPtr mwHandle = Autodesk.AdvanceSteel.CADAccess.CADUtilities.GetCADWindowHandle();
      return new DynamoView(dynamoViewModel);
    }

    private static bool initializedCore;

    private static void InitializeCore()
    {
      if (initializedCore) return;

      string path = System.Environment.GetEnvironmentVariable("PATH");
      System.Environment.SetEnvironmentVariable("PATH", path + ";" + DynamoAdvanceSteelApplication.DynamoCorePath);

      var preloader = new Preloader(DynamoAdvanceSteelApplication.DynamoCorePath, DynamoAdvanceSteelApplication.ACADCorePath, LibraryVersion.Version222);
      preloader.Preload();
      GeometryFactoryPath = preloader.GeometryFactoryPath;

      initializedCore = true;
    }
  }
}