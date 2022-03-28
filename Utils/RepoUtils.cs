using System;

namespace Utils
{
    public static class RepoUtils
    {
        public static TEntity CheckUpdateObject<TEntity>(TEntity originalObj, TEntity updateObj)
        {
            foreach (var property in updateObj.GetType().GetProperties())
            {
                if (property.GetValue(updateObj, null) == null)
                {
                    property.SetValue(updateObj, originalObj.GetType().GetProperty(property.Name)
                    .GetValue(originalObj, null));
                }
            }
            return updateObj;
        }
    }
}
