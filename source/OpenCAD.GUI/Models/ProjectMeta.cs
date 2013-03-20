using System;
using System.Collections.ObjectModel;
using System.IO;
using Caliburn.Micro;
using OpenCAD.GUI.Misc;
using OpenCAD.GUI.ViewModels;

namespace OpenCAD.GUI.Models
{
    public class ProjectMeta : PropertyChangedBase, IProjectMeta
    {
        private string _filePath;
        private string _name;
        private ObservableCollection<JsonPartMeta> _parts;
        private IReadOnlyObservableCollection<IPartMeta> _readOnlyParts;

        public ObservableCollection<JsonPartMeta> Parts {
            get { return _parts; }
            set {
                if (Equals(value, _parts)) return;
                _parts = value;
                _readOnlyParts = value.WrapReadOnly<JsonPartMeta, IPartMeta>();
                NotifyOfPropertyChange(() => Parts);
            }
        }

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

        IReadOnlyObservableCollection<IPartMeta> IProjectMeta.Parts {
            get { return _readOnlyParts; }
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