﻿// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using Ghpr.Core;
using Ghpr.Core.Enums;
using Ghpr.Core.Utils;
using Ghpr.Core.Interfaces;

namespace Ghpr.MSTestV2.Utils
{
    public class GhprMSTestV2RunHelper
    {
        public static void CreateReportFromFile(string path)
        {
            var reporter = new Reporter(TestingFramework.MSTest);
            try
            {
                var testRuns = GetTestRunsListFromFile(path);
                reporter.GenerateFullReport(testRuns);
            }
            catch (Exception ex)
            {
                var log = new Log(reporter.Settings.OutputPath);
                log.Exception(ex, "Exception in CreateReportFromFile");
            }
        }

        public static List<ITestRun> GetTestRunsListFromFile(string path)
        {
            try
            {
                var reader = new TrxReader(path);
                var testRuns = reader.GetTestRuns();
                return testRuns;
            }
            catch (Exception ex)
            {
                var reporter = new Reporter(TestingFramework.MSTest);
                var log = new Log(reporter.Settings.OutputPath);
                log.Exception(ex, "Exception in GetTestRunsListFromFile");
                return null;
            }
        }
    }
}