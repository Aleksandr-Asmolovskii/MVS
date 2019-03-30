using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary3
{
    public class Class1
    {
        [CommandMethod("MC")]
        public void Run()
        {
            //Создаем документ и привязываем его к активному окну
            Document doc = Application.DocumentManager.MdiActiveDocument;
            // Cоздаем ссылку на базу данных
            Database db = doc.Database;
            //Создаем ссылку на таблицу слоев
            ObjectId Layers = db.LayerTableId;
         
            List<string> LayerList = new List<string>();

            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                LayerTable layertable = tr.GetObject(Layers, OpenMode.ForRead) as LayerTable;
                foreach (ObjectId item in layertable)
                {
                    LayerTableRecord layerTableRecord = tr.GetObject(item, OpenMode.ForRead) as LayerTableRecord;
                    LayerList.Add(layerTableRecord.Name);
                }
                //обращаемся к командной строке
                Editor edit = doc.Editor;
                
                foreach (var item in LayerList)
                {
                    edit.WriteMessage("\n{0}",item);
                }
                tr.Commit();
            }
        }
    }
}
