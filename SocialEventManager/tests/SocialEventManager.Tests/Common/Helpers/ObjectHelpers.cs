using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SocialEventManager.Tests.Common.Helpers
{
    public static class ObjectHelpers
    {
        /// <summary>
        /// Compares the properties of two objects of the same type and returns if all properties are equal.
        /// </summary>
        /// <param name="objectA">The first object to compare.</param>
        /// <param name="objectB">The second object to compare.</param>
        /// <param name="ignoreList">A list of property names to ignore from the comparison.</param>
        /// <returns><c>true</c> if all property values are equal; otherwise, <c>false</c>.</returns>
        public static bool AreObjectsEqual(object objectA, object objectB, params string[] ignoreList)
        {
            bool result;

            if (objectA != null && objectB != null)
            {
                Type objectType = objectA.GetType();

                result = true;

                foreach (PropertyInfo propertyInfo in objectType.GetProperties(
                  BindingFlags.Public | BindingFlags.Instance).Where(
                  p => p.CanRead && !ignoreList.Contains(p.Name)))
                {
                    object valueA = propertyInfo.GetValue(objectA, null);
                    object valueB = propertyInfo.GetValue(objectB, null);

                    // if it is a primitive type, value type or implements IComparable, just directly try and compare the value.
                    if (CanDirectlyCompare(propertyInfo.PropertyType))
                    {
                        if (!AreValuesEqual(valueA, valueB))
                        {
                            result = false;
                        }
                    }
                    else if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                    {
                        IEnumerable<object> collectionItems1, collectionItems2;
                        int collectionItemsCount1, collectionItemsCount2;

                        if ((valueA == null && valueB != null) || (valueA != null && valueB == null))
                        {
                            result = false;
                        }
                        else if (valueA != null && valueB != null)
                        {
                            collectionItems1 = ((IEnumerable)valueA).Cast<object>();
                            collectionItems2 = ((IEnumerable)valueB).Cast<object>();
                            collectionItemsCount1 = collectionItems1.Count();
                            collectionItemsCount2 = collectionItems2.Count();

                            // Check the counts to ensure they match
                            if (collectionItemsCount1 != collectionItemsCount2)
                            {
                                result = false;
                            }

                            // And if they do, compare each item... This assumes both collections have the same order
                            else
                            {
                                for (int i = 0; i < collectionItemsCount1; i++)
                                {
                                    object collectionItem1;
                                    object collectionItem2;
                                    Type collectionItemType;

                                    collectionItem1 = collectionItems1.ElementAt(i);
                                    collectionItem2 = collectionItems2.ElementAt(i);
                                    collectionItemType = collectionItem1.GetType();

                                    if (CanDirectlyCompare(collectionItemType))
                                    {
                                        if (!AreValuesEqual(collectionItem1, collectionItem2))
                                        {
                                            result = false;
                                        }
                                    }
                                    else if (!AreObjectsEqual(collectionItem1, collectionItem2, ignoreList))
                                    {
                                        result = false;
                                    }
                                }
                            }
                        }
                    }
                    else if (propertyInfo.PropertyType.IsClass)
                    {
                        if (!AreObjectsEqual(propertyInfo.GetValue(objectA, null), propertyInfo.GetValue(objectB, null), ignoreList))
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
            }
            else
            {
                result = Equals(objectA, objectB);
            }

            return result;
        }

        #region Private Methods

        /// <summary>
        /// Determines whether value instances of the specified type can be directly compared.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this value instances of the specified type can be directly compared; otherwise, <c>false</c>.</returns>
        private static bool CanDirectlyCompare(Type type) =>
            typeof(IComparable).IsAssignableFrom(type) || type.IsPrimitive || type.IsValueType;

        /// <summary>
        /// Compares two values and returns if they are the same.
        /// </summary>
        /// <param name="valueA">The first value to compare.</param>
        /// <param name="valueB">The second value to compare.</param>
        /// <returns><c>true</c> if both values match; otherwise, <c>false</c>.</returns>
        private static bool AreValuesEqual(object valueA, object valueB)
        {
            IComparable selfValueComparer = valueA as IComparable;

            return !((valueA == null && valueB != null) || (valueA != null && valueB == null)
                || (selfValueComparer != null && selfValueComparer.CompareTo(valueB) != 0)
                || !Equals(valueA, valueB));
        }

        #endregion Private Methods
    }
}
