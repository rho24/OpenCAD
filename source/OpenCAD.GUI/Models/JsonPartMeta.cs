using System;
using System.IO;
using Caliburn.Micro;
using OpenCAD.GUI.ViewModels;

namespace OpenCAD.GUI.Models
{
    public class JsonPartMeta : PropertyChangedBase, IPartMeta
    {
        private string _filePath;

        public string FilePath {
            get { return _filePath; }
            set {
                if (value == _filePath) return;
                _filePath = value;
                NotifyOfPropertyChange(() => FilePath);
                NotifyPossibleChanges();

                InitializeWatcher();
            }
        }

        public string FileName {
            get { return Path.GetFileName(FilePath); }
        }

        public bool Exists {
            get { return File.Exists(FilePath); }
        }

        public DateTime? CreatedDate {
            get { return Exists ? (DateTime?) File.GetCreationTimeUtc(FilePath) : null; }
        }

        public DateTime? ModifiedDate {
            get { return Exists ? (DateTime?) File.GetLastWriteTimeUtc(FilePath) : null; }
        }

        public string Name {
            get { return Path.GetFileNameWithoutExtension(FilePath); }
        }

        private void NotifyPossibleChanges() {
            NotifyOfPropertyChange(() => FileName);
            NotifyOfPropertyChange(() => Name);
            NotifyOfPropertyChange(() => CreatedDate);
            NotifyOfPropertyChange(() => ModifiedDate);
            NotifyOfPropertyChange(() => Exists);
        }

        private void InitializeWatcher() {
            var watcher = new FileSystemWatcher(Path.GetDirectoryName(FilePath), FileName); //TODO: Should dispose
            watcher.Changed += (sender, args) => NotifyPossibleChanges();
            watcher.Created += (sender, args) => NotifyPossibleChanges();
            watcher.Deleted += (sender, args) => NotifyPossibleChanges();
            watcher.Renamed += (sender, args) => NotifyPossibleChanges();
            watcher.EnableRaisingEvents = true;
        }
    }
}