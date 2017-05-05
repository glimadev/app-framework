using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vitali.Framework.Helper
{
    public static class LogHelper
    {
        //    public static LogUpdates CompareObjects<T>(T self, T to, int userId)
        //    {
        //        DateTime today = DateTime.Now;

        //        LogUpdates logUpdates = new LogUpdates();

        //        var type = typeof(T);

        //        if (self != null && to != null)
        //        {
        //            var changedFields = (from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
        //                                 let Property = type.GetProperty(pi.Name).Name
        //                                 let ValueOld = type.GetProperty(pi.Name).GetValue(self, null)
        //                                 let ValueNew = type.GetProperty(pi.Name).GetValue(to, null)
        //                                 let Type = type.GetProperty(pi.Name).GetType()
        //                                 where ValueOld != ValueNew
        //                                       && (ValueOld == null || !ValueOld.Equals(ValueNew))
        //                                       && pi.Name != "EditedDate"
        //                                       && (type.GetProperty(pi.Name).GetCustomAttributes(typeof(NotMappedAttribute), false).Length == 0)
        //                                       && (ValueNew != null)//&& !ValueNew.GetType().Name.Contains("List"))
        //                                 //&& (ValueOld != null)//&& !ValueOld.GetType().Name.Contains("List"))
        //                                 //&& (ValueOld != null)// && !ValueOld.GetType().Name.Contains("Vitali"))
        //                                 select new
        //                                 {
        //                                     Type = Type,
        //                                     Property = Property,
        //                                     ValueOld = ValueOld,
        //                                     ValueNew = ValueNew
        //                                 });

        //            foreach (var changedField in changedFields)
        //            {
        //                if (changedField.ValueNew != null)
        //                {

        //                    if (changedField.ValueNew == null
        //                        || changedField.ValueNew.GetType().Equals(typeof(string))
        //                        || changedField.ValueNew.GetType().Equals(typeof(int))
        //                        || changedField.ValueNew.GetType().Equals(typeof(decimal))
        //                        || changedField.ValueNew.GetType().Equals(typeof(float))
        //                        || changedField.ValueNew.GetType().Equals(typeof(bool))
        //                        || changedField.ValueNew.GetType().Equals(typeof(int?))
        //                        || changedField.ValueNew.GetType().Equals(typeof(decimal?))
        //                        || changedField.ValueNew.GetType().Equals(typeof(bool?))
        //                        || changedField.ValueNew.GetType().Equals(typeof(float?))
        //                        || changedField.ValueNew.GetType().Equals(typeof(DateTime))
        //                        || changedField.ValueNew.GetType().Name.Contains("List")
        //                    )
        //                    {
        //                        string ValueOld = changedField.ValueOld == null ? "Vazio" : changedField.ValueOld.ToString();
        //                        string ValueNew = changedField.ValueNew == null ? "Vazio" : changedField.ValueNew.ToString();

        //                        if (changedField.ValueNew.GetType().Equals(typeof(decimal)) || changedField.ValueNew.GetType().Equals(typeof(decimal?)))
        //                        {
        //                            ValueOld = changedField.ValueOld == null ? "Vazio" : (Convert.ToDecimal(changedField.ValueOld)).ToString("C", CultureInfo.CurrentCulture);
        //                            ValueNew = changedField.ValueNew == null ? "Vazio" : (Convert.ToDecimal(changedField.ValueNew)).ToString("C", CultureInfo.CurrentCulture);
        //                        }

        //                        if (changedField.ValueNew.GetType().Equals(typeof(float)) || changedField.ValueNew.GetType().Equals(typeof(float?)))
        //                        {
        //                            ValueOld = changedField.ValueOld == null ? "Vazio" : Convert.ToDecimal(changedField.ValueOld) + "%";
        //                            ValueNew = changedField.ValueNew == null ? "Vazio" : Convert.ToDecimal(changedField.ValueNew) + "%";
        //                        }

        //                        if (changedField.ValueNew.GetType().Equals(typeof(DateTime)) || changedField.ValueNew.GetType().Equals(typeof(DateTime?)))
        //                        {
        //                            ValueOld = changedField.ValueOld == null ? "Vazio" : (Convert.ToDateTime(changedField.ValueOld)).ToString("dd/MM/yyyy");
        //                            ValueNew = changedField.ValueNew == null ? "Vazio" : (Convert.ToDateTime(changedField.ValueNew)).ToString("dd/MM/yyyy");
        //                        }

        //                        if (changedField.ValueNew.GetType().Equals(typeof(bool)) || changedField.ValueNew.GetType().Equals(typeof(bool?)))
        //                        {
        //                            ValueOld = changedField.ValueOld == null ? "Vazio" : (Convert.ToBoolean(changedField.ValueOld)) ? "Sim" : "Não";
        //                            ValueNew = changedField.ValueNew == null ? "Vazio" : (Convert.ToBoolean(changedField.ValueNew)) ? "Sim" : "Não";
        //                        }

        //                        try
        //                        {
        //                            string resValue = Resource.GetValue(changedField.Property + "Rel");

        //                            if (resValue != null && resValue.Length > 1)
        //                            {
        //                                ValueOld = "";
        //                                ValueNew = "";

        //                                var commentSplited = resValue.Replace("\"", "").Split('-');

        //                                string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

        //                                Type typeOfAlteredField = Type.GetType("Vitali.DFG.Infrastructure.Data.DataAccess." + commentSplited[1] + ", " + assemblyName);

        //                                //1x1
        //                                if (commentSplited[0] == "1")
        //                                {
        //                                    if (changedField.ValueOld != null && changedField.ValueNew != null && typeOfAlteredField != null)
        //                                    {
        //                                        DbSet dbSetFromType = Db.Set(typeOfAlteredField);

        //                                        var findedValueOld = dbSetFromType.Find(changedField.ValueOld);

        //                                        var findedValueNew = dbSetFromType.Find(changedField.ValueNew);

        //                                        if (findedValueOld != null)
        //                                        {
        //                                            ValueOld = typeOfAlteredField.GetProperty(commentSplited[2]).GetValue(findedValueOld, null).ToString();
        //                                        }

        //                                        if (findedValueNew != null)
        //                                        {
        //                                            ValueNew = typeOfAlteredField.GetProperty(commentSplited[2]).GetValue(findedValueNew, null).ToString();
        //                                        }
        //                                    }
        //                                }

        //                                //1Xn
        //                                if (commentSplited[0] == "n")
        //                                {
        //                                    ValueOld = "<br/>";
        //                                    ValueNew = "<br/>";

        //                                    var valueNewIEnumerable = (IEnumerable)(changedField.ValueNew);
        //                                    var valueOldIEnumerable = (IEnumerable)(changedField.ValueOld);


        //                                    foreach (var oldItem in valueOldIEnumerable)
        //                                    {
        //                                        var valores = String.Empty;

        //                                        for (var i = 2; i < commentSplited.Length; i++)
        //                                        {
        //                                            valores += typeOfAlteredField.GetProperty(commentSplited[i]).GetValue(oldItem, null).ToString() + " ";
        //                                        }

        //                                        ValueOld += valores + " <br/> ";
        //                                    }

        //                                    foreach (var newItem in valueNewIEnumerable)
        //                                    {
        //                                        var valores = String.Empty;

        //                                        for (var i = 2; i < commentSplited.Length; i++)
        //                                        {
        //                                            valores += typeOfAlteredField.GetProperty(commentSplited[i]).GetValue(newItem, null).ToString() + " ";
        //                                        }

        //                                        ValueNew += valores + " <br/> ";
        //                                    }

        //                                    if (ValueNew == "<br/>")
        //                                    {
        //                                        ValueNew += " - ";
        //                                    }

        //                                    if (ValueOld == "<br/>")
        //                                    {
        //                                        ValueOld += " - ";
        //                                    }
        //                                }
        //                            }

        //                            if (ValueNew != ValueOld)
        //                            {
        //                                logUpdates.LogUpdatesItens.Add(new LogUpdatesItens
        //                                {
        //                                    ValueNew = (String.IsNullOrEmpty(ValueNew)) ? changedField.ValueNew.ToString() : ValueNew,
        //                                    ValueOld = (String.IsNullOrEmpty(ValueOld)) ? changedField.ValueOld.ToString() : ValueOld,
        //                                    Property = changedField.Property,
        //                                    CreatedDate = today,
        //                                    Deleted = false,
        //                                });
        //                            }
        //                        }
        //                        catch { }
        //                    }
        //                    else
        //                    {
        //                        try
        //                        {
        //                            Type typeA = changedField.ValueOld.GetType();
        //                            Type typeB = changedField.ValueNew.GetType();

        //                            var objOld = Convert.ChangeType(changedField.ValueOld, typeB);
        //                            var objNew = Convert.ChangeType(changedField.ValueNew, typeB);

        //                            var a = objOld as TEntity;
        //                            var b = objNew as TEntity;


        //                            CompareObjects(a, b, userId);

        //                        }
        //                        catch (Exception)
        //                        {

        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        logUpdates.EntityType = type.Name;
        //        logUpdates.CreatedDate = today;
        //        logUpdates.Deleted = false;
        //        logUpdates.UserId = userId;

        //        ObjectContext objectContext = ((IObjectContextAdapter)Db).ObjectContext;
        //        ObjectSet<TEntity> set = objectContext.CreateObjectSet<TEntity>();

        //        string keyName = set.EntitySet.ElementType.KeyMembers.Select(k => k.Name).First();

        //        PropertyInfo prop = type.GetProperty(keyName);
        //        PropertyInfo prop2 = type.GetProperty("ClientId");

        //        logUpdates.EntityId = Convert.ToInt32(prop.GetValue(self));
        //        logUpdates.ClientId = Convert.ToInt32(prop2.GetValue(self));

        //        return logUpdates;
        //    }
        //}

        //public partial class LogUpdates
        //{
        //    public int LogUpdateId { get; set; } // LogUpdateId (Primary key)

        //    public int? UserId { get; set; } // UserId

        //    public int? ClientId { get; set; } // ClientId

        //    public int EntityId { get; set; } // EntityId

        //     public string EntityType { get; set; } // EntityType

        //    public DateTime CreatedDate { get; set; } // CreatedDate

        //    public string Detail { get; set; } // Detail
        //}
    }
}
