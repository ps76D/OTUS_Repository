using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine;
using SaveSystem.Data;

namespace SaveSystem
{
    [Serializable]
    public class ResourcesSaveLoader : SaveLoader<ResourceService, ResourceData[]>
    {
        override protected ResourceData[] ConvertToData(ResourceService service)
        {
            var resources = service.GetResources();
            
            var resourceArray = resources.ToArray();
            
            ResourceData[] dataArray = new ResourceData[resourceArray.Length];

            for (int i = 0; i < resourceArray.Length; i++)
            {
                var resource = resourceArray[i];
                dataArray[i] = new ResourceData
                {
                    _id = resource.ID,
                    _amount = resource.Amount
                };
            }

            return dataArray;
        }

        override protected void SetupData(ResourceService service, ResourceData[] dataArray)
        {
            IEnumerable<Resource> resources = service.GetResources();

            var enumerable = resources as Resource[] ?? resources.ToArray();
            foreach (var resource in enumerable)
            {
                var data = dataArray.FirstOrDefault(d => d._id == resource.ID);
                resource.Amount = data._amount;
            }
            
            service.SetResources(enumerable);
        }
        
        /*override protected void SetupDefaultData(ResourceService service)
        {
            Debug.Log($"<color=yellow>Setup default data</color>");
        }*/
    }
}
