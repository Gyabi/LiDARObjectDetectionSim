using System.Collections.Generic;

namespace Gyabi.LiDAR
{
    public class DataStore
    {
        private List<float[]> _lidarDataStore = new List<float[]>();

        public void AddData(float[] data)
        {
            this._lidarDataStore.Add(data);
        }

        public List<float[]> GetData()
        {
            List<float[]> data = new List<float[]>(this._lidarDataStore);

            return data;
        }
    }
}