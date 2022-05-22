using Newtonsoft.Json;

namespace ApiParameter_V1.Helper
{
    public static class DataBindingHelper
    {
        private static readonly IDictionary<Type, DataBinderDelegate> TypeBinders = new Dictionary<Type,DataBinderDelegate>();

        public static bool BindData(string parameterKey,string stringData, Type type, out object data)
        {
            data = null;
            if (type.IsArray)
            {
                Type elementType = type.GetElementType();
                return DataBindingHelper.BindArrayData(stringData, elementType, out data);
            }
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = type.GetGenericArguments()[0];
            DataBindingHelper.DataBinderDelegate dataBinderDelegate;
            if (DataBindingHelper.TypeBinders.TryGetValue(type, out dataBinderDelegate))
                return dataBinderDelegate(stringData, out data);
            return type.IsEnum ? BindEnumData(stringData, type, out data) : BindObjectData(parameterKey, stringData, type, out data);
        }

        private static bool BindArrayData(string stringData, Type elementType, out object data)
        {
            data = (object)null;
            Type elementBaseType = elementType;
            if (elementType.IsGenericType && elementType.GetGenericTypeDefinition() == typeof(Nullable<>))
                elementBaseType = elementType.GetGenericArguments()[0];
            DataBindingHelper.DataBinderDelegate dataBinderDelegate;
            if (!DataBindingHelper.TypeBinders.TryGetValue(elementBaseType, out dataBinderDelegate))
            {
                if (!elementType.IsEnum)
                    return false;
                dataBinderDelegate = (string enumStringdata, out object enumData) => BindEnumData(enumStringdata, elementBaseType, out enumData);
            }
            string[] strArray = stringData.Split(',');
            Array instance = Array.CreateInstance(elementType, strArray.Length);
            for (int index = 0; index < strArray.Length; ++index)
            {
                object data1;
                if (!dataBinderDelegate(strArray[index], out data1))
                    return false;
                instance.SetValue(data1, index);
            }
            data = instance;
            return true;
        }
        private static bool BindEnumData(string stringData, Type enumType, out object data)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(stringData))
                {
                    data = Activator.CreateInstance(enumType);
                    return true;
                }
                data = Enum.Parse(enumType, stringData, true);
                if (Enum.IsDefined(enumType, data))
                    return true;
            }
            catch
            {
            }
            data = null;
            return false;
        }

        private static bool BindObjectData(string parameterKey,string stringData,Type elementType,out object data)
        {
            data = null;
            try
            {
                data = JsonConvert.DeserializeObject(stringData, elementType);
                return true;
            }
            catch (UnprocessableEntityException ex)
            {
                throw;
            }
            catch (JsonSerializationException ex)
            {
                if (ex.InnerException is UnprocessableEntityException)
                    throw ex.InnerException;
                int? nullable1 = ex.Message?.IndexOf("not found in JSON");
                int? nullable2 = nullable1;
                int num1 = -1;
                if (nullable2.GetValueOrDefault() > num1 & nullable2.HasValue)
                {
                    string propertyName = ex.GetPropertyName(parameterKey, nullable1.Value);
                    throw new UnprocessableEntityException("missing_" + propertyName, propertyName + " is required.", propertyName);
                }
                int? nullable3 = ex.Message?.IndexOf("expects a non-null value");
                int? nullable4 = nullable3;
                int num2 = -1;
                if (nullable4.GetValueOrDefault() == num2 & nullable4.HasValue)
                    nullable3 = ex.Message?.IndexOf("expects a value but got null");
                int? nullable5 = nullable3;
                int num3 = -1;
                if (nullable5.GetValueOrDefault() > num3 & nullable5.HasValue)
                {
                    string propertyName = ex.GetPropertyName(parameterKey, nullable3.Value);
                    throw new UnprocessableEntityException("invalid_" + propertyName, "value of " + propertyName + " is required.", propertyName);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private static string GetPropertyName(this JsonSerializationException exception,string parameterKey,int indexOfEndPropertyName)
        {
            int startIndex = exception.Message.IndexOf('\'');
            string str = exception.Message.Substring(startIndex, indexOfEndPropertyName - startIndex).Trim();
            return (string.IsNullOrWhiteSpace(parameterKey) ? "" : parameterKey + ".") + (string.IsNullOrWhiteSpace(exception.Path) ? "" : exception.Path + ".") + str.Substring(1, str.Length - 2);
        }

        private delegate bool DataBinderDelegate(string stringData, out object data);
    }
}
