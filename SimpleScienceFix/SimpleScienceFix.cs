using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleScienceFix
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class SimpleScienceFix : MonoBehaviour
    {
        private void Update()
        {
            if (FlightGlobals.ActiveVessel.FindPartModulesImplementing<ModuleScienceExperiment>() != null)
            {
                foreach (ModuleScienceExperiment exp in FlightGlobals.ActiveVessel.FindPartModulesImplementing<ModuleScienceExperiment>())
                {
                    if (exp.experimentID == "crewReport" | exp.experimentID == "surfaceSample" | exp.experimentID == "evaReport")
                    {
                        if (exp.part.FindModuleImplementing<ModuleScienceContainer>() != null)
                        {
                            if (exp.GetData() != null)
                            {
                                foreach (ScienceData data in exp.GetData())
                                {
                                    List<ModuleScienceExperiment> explist = new List<ModuleScienceExperiment>();
                                    explist.Add(exp);
                                    if (exp.part.FindModuleImplementing<ModuleScienceContainer>().HasData(data) == false)
                                        exp.part.FindModuleImplementing<ModuleScienceContainer>().StoreData(explist.Cast<IScienceDataContainer>().ToList(), true);
                                    explist.Clear();
                                    exp.ResetExperiment();
                                } 
                            }
                        }
                    }
                }
            }
        }

    }
}
