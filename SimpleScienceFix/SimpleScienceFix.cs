using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleScienceFix
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class SimpleScienceFix :MonoBehaviour
    {
        private void Update()
        {
            foreach (ModuleScienceExperiment exp in FlightGlobals.ActiveVessel.FindPartModulesImplementing<ModuleScienceExperiment>())
            {
                if (exp.experimentID == "crewReport" | exp.experimentID == "surfaceSample" | exp.experimentID == "evaReport")
                {
                    List<ModuleScienceExperiment> explist = new List<ModuleScienceExperiment>();
                    explist.Add(exp);
                    exp.part.Modules.GetModules<ModuleScienceContainer>()[0].StoreData(explist.Cast<IScienceDataContainer>().ToList(), true);
                    exp.ResetExperiment();
                    explist.Clear();
                }
            }
        }

    }
}
