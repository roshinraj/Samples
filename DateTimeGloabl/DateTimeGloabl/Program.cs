using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeGloabl
{
    public class Folder 
    {
        public long FolderID { get; set; }
        public long ParentID { get; set; }
        public string FolderName { get; set; }
        public long FolderCategoryID { get; set; }
        public string Icon { get; set; }
        public string IsCollapsed { get; set; }
        
        public int Level { get; set; }

        public virtual List<Folder> Folders { get; set; }        
    }

    public class FolderTree
    {
        #region Singleton
        private static FolderTree instance = null;
        private static object syncRoot = new object();
        private FolderTree()
        {
        }
        public static FolderTree Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new FolderTree();
                    }
                }
                return instance;
            }
        }
        #endregion

        public List<Folder> GetFolderTree(IEnumerable<Folder> source)
        {
            var treeCollection = source.Where(x => x.ParentID == 0).ToList();
            source = source.Where(x => x.ParentID != 0);
            treeCollection.ForEach(f =>
            {
                f.Level = 0;
                ConstructFolderTree(source, f);
            });
            return treeCollection;
        }

        private void ConstructFolderTree(IEnumerable<Folder> source, Folder folder)
        {
            folder.Folders = source.Where(x => x.ParentID == folder.FolderID).ToList();
            source = source.Where(x => x.ParentID != folder.FolderID);
            folder.Folders.ForEach(f =>
            {
                f.Level = folder.Level + 1;
                ConstructFolderTree(source, f);
            });
        }
    }

    class A
    {
        public int ID { get; set; }
    }
    class B:A
    {
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Folder> folders = new List<Folder>();

            folders.Add(new Folder() { FolderID = 1, FolderName = "A", ParentID = 0 });
            folders.Add(new Folder() { FolderID = 2, FolderName = "B", ParentID = 0 });
            folders.Add(new Folder() { FolderID = 3, FolderName = "A1", ParentID = 1 });
            folders.Add(new Folder() { FolderID = 4, FolderName = "A2", ParentID = 1 });
            folders.Add(new Folder() { FolderID = 5, FolderName = "B1", ParentID = 2 });
            folders.Add(new Folder() { FolderID = 6, FolderName = "B2", ParentID = 2 });
            folders.Add(new Folder() { FolderID = 7, FolderName = "A1.1", ParentID = 3 });
            folders.Add(new Folder() { FolderID = 8, FolderName = "A1.2", ParentID = 3 });
            folders.Add(new Folder() { FolderID = 9, FolderName = "B1.1", ParentID = 5 });
            folders.Add(new Folder() { FolderID = 1, FolderName = "B1.2", ParentID = 5 });
            var res = FolderTree.Instance.GetFolderTree(folders);
            string s = string.Empty;
            char c = char.MaxValue;
            char c1 = char.MinValue;
            char c3 = '\0';
            //A a = new DateTimeGloabl.A() { ID = 1 };
            //B b = (B)a;
            var d1 = DateTime.Now;
            var d2 = DateTime.UtcNow;
            var d3 = DateTimeOffset.Now;
            var d4 = DateTimeOffset.UtcNow;

            var d5 = d1.ToUniversalTime();
            var d6 = d2.ToUniversalTime();
            var d7 = d3.ToUniversalTime();
            var d8 = d4.ToUniversalTime();

            DateTimeOffset dto;
            DateTimeOffset.TryParse("2017-05-23 13:07:07.4061605 +05:30", out dto);

        }
    }
}
