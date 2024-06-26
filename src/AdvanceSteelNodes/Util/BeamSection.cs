﻿using Autodesk.AdvanceSteel.CADAccess;
using Autodesk.AdvanceSteel.Modelling;
using Autodesk.AdvanceSteel.ConstructionTypes;

namespace AdvanceSteel.Nodes.Util
{
	/// <summary>
	/// This node can be used to assign Section to Advance Steel beams from Dynamo
	/// </summary>
	public class BeamSection
	{
		internal BeamSection()
		{
		}
		/// <summary>
		/// This node can set the Section for Advance Steel beams from Dynamo.
		/// For the Section use the following format: "HEA  DIN18800-1#@§@#HEA100"
		/// </summary>
		/// <param name="beamElement">Advance Steel beam</param>
		/// <param name="sectionName">Section</param>
		/// <returns></returns>
		public static void SetSection(AdvanceSteel.Nodes.Object beamElement, string sectionName)
		{
			//lock the document and start transaction
			using (var _CADAccess = new AdvanceSteel.Services.ObjectAccess.CADContext())
			{
				string handle = beamElement.Handle;

				FilerObject obj = Utils.GetObject(handle);

				if (obj != null && obj.IsKindOf(FilerObject.eObjectType.kBeam))
				{

					string sectionType = string.Empty;
					string sectionSize = string.Empty;

					string separator = "#@" + '§' + "@#";
					string[] section = sectionName.Split(new string[] { separator }, System.StringSplitOptions.None);

					if (section.Length == 2)
					{
						sectionType = section[0];
						sectionSize = section[1];
					}

					Beam beam = obj as Beam;
					beam.ChangeProfile(sectionType, sectionSize);
				}

				else
					throw new System.Exception("Failed to change section");
			}
		}
	}
}