using SRA.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SRA.Client.ViewModel
{
    public class PlateViewModel: IPlateViewModel
    {
        public PlateModelCollection Plates => Repo.Plates;

        Dictionary<int, PlateModelCollection> _plateRows = new Dictionary<int, PlateModelCollection>();
        public Dictionary<int, PlateModelCollection> PlateRows
        {
            get
            {
                if (Plates!= null && Plates.Count == 16)
                {
                    _plateRows = new Dictionary<int, PlateModelCollection>(16);
                    for (int i = 0; i < 4; i++)
                    {
                        _plateRows.Add(i, new PlateModelCollection());
                        for (int j = 0; j < 4; j++)
                        {
                            _plateRows[i].Add(this.Plates[i * 4 + j]);
                        }
                    }
                }

                return _plateRows;
            }
        }

        IModelRepository Repo;

        public event EventHandler PlatesChanged;

        protected virtual void OnPlatesChanged()
        {
            PlatesChanged?.Invoke(this, EventArgs.Empty);
        }

        public PlateViewModel(IModelRepository repo)
        {
            Repo = repo;
            Repo.PlatesChanged += Repo_PlatesChanged;
        }

        private void Repo_PlatesChanged(object sender, EventArgs e)
        {
            OnPlatesChanged();
        }
    }
}
