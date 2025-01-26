using System;
using System.Linq;
using GameEngine;
using SaveSystem.Data;
using UnityEngine;
using Zenject;

namespace SaveSystem
{
    [Serializable]
    public class UnitsSaveLoader : SaveLoader<UnitManager, UnitData[]>
    {
        [Inject]
        private UnitsPrefabs _unitsPrefabs;
        
        override protected UnitData[] ConvertToData(UnitManager service)
        {
            var units = service.GetAllUnits();

            var unitsArray = units.ToArray();
            
            UnitData[] unitDataArray = new UnitData[unitsArray.Length];

            for (int i = 0; i < unitsArray.Length; i++)
            {
                var unit = unitsArray[i];
                unitDataArray[i] = new UnitData
                {
                    _name = unit.gameObject.name,
                    _type = unit.Type,
                    
                    _hitPoints = unit.HitPoints,
                    
                    _xPosition = unit.gameObject.transform.position.x,
                    _yPosition = unit.gameObject.transform.position.y,
                    _zPosition = unit.gameObject.transform.position.z,
                    
                    _xRotation = unit.gameObject.transform.rotation.eulerAngles.x,
                    _yRotation = unit.gameObject.transform.rotation.eulerAngles.y,
                    _zRotation = unit.gameObject.transform.rotation.eulerAngles.z,
                };
            }
            
            return unitDataArray;
        }

        override protected void SetupData(UnitManager service, UnitData[] dataArray)
        {
            var units = service.GetAllUnits();
            
            var enumerable = units as Unit[] ?? units.ToArray();

            foreach (var unitData in dataArray)
            {
                var position = new Vector3(unitData._xPosition, unitData._yPosition, unitData._zPosition);
                var rotation = new Vector3(unitData._xRotation, unitData._yRotation, unitData._zRotation);

                Unit unit = enumerable.FirstOrDefault(u => u != null && u.name == unitData._name);

                if (unit == null)
                {
                    SpawnSavedUnits(service, out unit, unitData, position, rotation);
                }

                unit.HitPoints = unitData._hitPoints;
                unit.gameObject.transform.position = position;
                unit.gameObject.transform.rotation = Quaternion.Euler(rotation);
            }

            service.SetupUnits(enumerable);

            DeleteUnsavedUnits(service, enumerable, dataArray);
        }

        private void SpawnSavedUnits(UnitManager service, out Unit unit, UnitData unitData, Vector3 position, Vector3 rotation)
        {
            var unitPrefab = _unitsPrefabs.Units.FirstOrDefault(prefab => prefab.name == unitData._type);

            var newUnit = service.SpawnUnit(unitPrefab, position, Quaternion.Euler(rotation));

            unit = newUnit;
        }
        
        private void DeleteUnsavedUnits(UnitManager service, Unit[] enumerable, UnitData[] dataArray)
        {
            var difference = enumerable
                .Where(unit => dataArray.All(unitData => unitData._name != unit.name))
                .ToArray();
            
            foreach (var unit in difference)
            {
                service.DestroyUnit(unit);
            }
        }
    }
}
