/////////////////////////////////////////////////////////////////////////////////////////
//
// Please refer to "COPYRIGHT.md" for the relevant copyright statement of this software.
//
/////////////////////////////////////////////////////////////////////////////////////////
//

using GrxCAD.Runtime;
using GrxCAD.ApplicationServices;
using GrxCAD.DatabaseServices;
using GrxCAD.Geometry;
using System.IO;
//Imports Autodesk.AutoCAD.Runtime
//Imports Autodesk.AutoCAD.ApplicationServices
//Imports Autodesk.AutoCAD.DatabaseServices
//Imports Autodesk.AutoCAD.Geometry

[assembly: CommandClass(typeof(hello.HelloCmd))]

namespace hello
{
  public class HelloCmd
  {
    [CommandMethod("Hello")]
    static public void DoIt()
    {

      try
      {
        //Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Hello dotnet");
                /*              Dim acDoc As Document =*/
               // Document acDoc = Application.DocumentManager.MdiActiveDocument;
                //Database acCurDb = acDoc.Database;
                Document acDoc = Application.DocumentManager.Open("E:\\DDRive\\CBIT\\gstarCAD\\Innersixangleflatcountersunkheadscrews(B).dwg",
                    false, null);
                Database acCurDb = acDoc.Database;

                // Open the Block table for read
                //BlockTable acBlkTbl = null;
                //acBlkTbl = acCurDb.BlockTableId.Open(OpenMode.ForRead) as BlockTable;
                //// Open the Block table record Model space for read
                //BlockTableRecord acBlkTblRec = null;

                //acBlkTblRec = acBlkTbl[BlockTableRecord.ModelSpace].Open(OpenMode.ForRead) as BlockTableRecord;

                //    foreach (ObjectId acObjId in acBlkTblRec)
                //{
                //    acDoc.Editor.WriteMessage("\nDXF name: " + acObjId.ObjectClass.DxfName);
                //    acDoc.Editor.WriteMessage("\nObjectID: " + acObjId.ToString());
                //    acDoc.Editor.WriteMessage("\nHandle: " + acObjId.Handle.ToString());
                //    acDoc.Editor.WriteMessage("\n");
                //}
                using (Transaction acTrans = acCurDb.TransactionManager.StartTransaction())
                {
                    // Open the Layer table for read
                    LayerTable acLyrTbl;
                    acLyrTbl = acTrans.GetObject(acCurDb.LayerTableId,
                                                    OpenMode.ForRead) as LayerTable;

                    string sLayerName = "VARUNLAYERTURNON";
                    if (acLyrTbl.Has(sLayerName) == false)
                    {
                        using (LayerTableRecord acLyrTblRec = new LayerTableRecord())
                        {
                            // Assign the layer a name
                            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("when did the layer select dotnet");
                            acLyrTblRec.Name = sLayerName;

                            // Turn the layer off
                            acLyrTblRec.IsOff = false;

                            // Upgrade the Layer table for write
                            acTrans.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite);

                            // Append the new layer to the Layer table and the transaction
                            acLyrTbl.Add(acLyrTblRec);
                            acTrans.AddNewlyCreatedDBObject(acLyrTblRec, true);
                            Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Hello did the layer select dotnet");

                        }
                    }
                    else
                    {
                        LayerTableRecord acLyrTblRec = acTrans.GetObject(acLyrTbl[sLayerName],
                                                        OpenMode.ForWrite) as LayerTableRecord;

                        // Turn the layer off
                        acLyrTblRec.IsOff = true;
                        Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage("Hello off dotnet");

                    }
                    acTrans.Commit();
                
                }

      }
            catch (System.Exception ex)
      {
        System.Windows.Forms.MessageBox.Show(ex.ToString());
      }
    }
  }
}
