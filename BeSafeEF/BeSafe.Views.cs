//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity.Infrastructure.MappingViews;

[assembly: DbMappingViewCacheTypeAttribute(
    typeof(EF6CodeFirstDemo.BeSafeContainer),
    typeof(Edm_EntityMappingGeneratedViews.ViewsForBaseEntitySetsac956b79e67df9fbc1adcf73ed0b06c69b78fd4be8f1902fda993746f31f1a26))]

namespace Edm_EntityMappingGeneratedViews
{
    using System;
    using System.CodeDom.Compiler;
    using System.Data.Entity.Core.Metadata.Edm;

    /// <summary>
    /// Implements a mapping view cache.
    /// </summary>
    [GeneratedCode("Entity Framework 6 Power Tools", "0.9.2.0")]
    internal sealed class ViewsForBaseEntitySetsac956b79e67df9fbc1adcf73ed0b06c69b78fd4be8f1902fda993746f31f1a26 : DbMappingViewCache
    {
        /// <summary>
        /// Gets a hash value computed over the mapping closure.
        /// </summary>
        public override string MappingHashValue
        {
            get { return "ac956b79e67df9fbc1adcf73ed0b06c69b78fd4be8f1902fda993746f31f1a26"; }
        }

        /// <summary>
        /// Gets a view corresponding to the specified extent.
        /// </summary>
        /// <param name="extent">The extent.</param>
        /// <returns>The mapping view, or null if the extent is not associated with a mapping view.</returns>
        public override DbMappingView GetView(EntitySetBase extent)
        {
            if (extent == null)
            {
                throw new ArgumentNullException("extent");
            }

            var extentName = extent.EntityContainer.Name + "." + extent.Name;

            if (extentName == "BeSafeStoreContainer.MasterItemsSet")
            {
                return GetView0();
            }

            if (extentName == "BeSafeContainer.MasterItemsSet")
            {
                return GetView1();
            }

            return null;
        }

        /// <summary>
        /// Gets the view for BeSafeStoreContainer.MasterItemsSet.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView0()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing MasterItemsSet
        [BeSafe.Store.MasterItemsSet](T1.MasterItemsSet_Id, T1.MasterItemsSet_name, T1.MasterItemsSet_description)
    FROM (
        SELECT 
            T.Id AS MasterItemsSet_Id, 
            T.name AS MasterItemsSet_name, 
            T.description AS MasterItemsSet_description, 
            True AS _from0
        FROM BeSafeContainer.MasterItemsSet AS T
    ) AS T1");
        }

        /// <summary>
        /// Gets the view for BeSafeContainer.MasterItemsSet.
        /// </summary>
        /// <returns>The mapping view.</returns>
        private static DbMappingView GetView1()
        {
            return new DbMappingView(@"
    SELECT VALUE -- Constructing MasterItemsSet
        [BeSafe.MasterItems](T1.MasterItems_Id, T1.MasterItems_name, T1.MasterItems_description)
    FROM (
        SELECT 
            T.Id AS MasterItems_Id, 
            T.name AS MasterItems_name, 
            T.description AS MasterItems_description, 
            True AS _from0
        FROM BeSafeStoreContainer.MasterItemsSet AS T
    ) AS T1");
        }
    }
}
