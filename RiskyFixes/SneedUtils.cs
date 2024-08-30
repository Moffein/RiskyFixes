using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyFixes
{
    public static class SneedUtils
    {
        public static void DumpEntityStateConfig(EntityStateConfiguration esc)
        {

            for (int i = 0; i < esc.serializedFieldsCollection.serializedFields.Length; i++)
            {
                if (esc.serializedFieldsCollection.serializedFields[i].fieldValue.objectValue)
                {
                    Debug.Log(esc.serializedFieldsCollection.serializedFields[i].fieldName + " - " + esc.serializedFieldsCollection.serializedFields[i].fieldValue.objectValue);
                }
                else
                {
                    Debug.Log(esc.serializedFieldsCollection.serializedFields[i].fieldName + " - " + esc.serializedFieldsCollection.serializedFields[i].fieldValue.stringValue);
                }
            }
        }
        public static void DumpEntityStateConfig(string entityStateName)
        {
            EntityStateConfiguration esc = LegacyResourcesAPI.Load<EntityStateConfiguration>("entitystateconfigurations/" + entityStateName);
            DumpEntityStateConfig(esc);
        }


        public static void DumpAddressableEntityStateConfig(string addressablePath)
        {
            EntityStateConfiguration esc = Addressables.LoadAssetAsync<EntityStateConfiguration>(addressablePath).WaitForCompletion();
            DumpEntityStateConfig(esc);
        }
        public static bool SetAddressableEntityStateField(string fullEntityStatePath, string fieldName, string value)
        {
            EntityStateConfiguration esc = Addressables.LoadAssetAsync<EntityStateConfiguration>(fullEntityStatePath).WaitForCompletion();
            for (int i = 0; i < esc.serializedFieldsCollection.serializedFields.Length; i++)
            {
                if (esc.serializedFieldsCollection.serializedFields[i].fieldName == fieldName)
                {
                    esc.serializedFieldsCollection.serializedFields[i].fieldValue.stringValue = value;
                    return true;
                }
            }
            return false;
        }

        public static bool SetAddressableEntityStateField(string fullEntityStatePath, string fieldName, Object newObject)
        {
            EntityStateConfiguration esc = Addressables.LoadAssetAsync<EntityStateConfiguration>(fullEntityStatePath).WaitForCompletion();
            for (int i = 0; i < esc.serializedFieldsCollection.serializedFields.Length; i++)
            {
                if (esc.serializedFieldsCollection.serializedFields[i].fieldName == fieldName)
                {
                    esc.serializedFieldsCollection.serializedFields[i].fieldValue.objectValue = newObject;
                    return true;
                }
            }
            return false;
        }
    }
}
