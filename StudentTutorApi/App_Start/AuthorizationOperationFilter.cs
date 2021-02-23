using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace StudentTutorApi.App_Start
{
    public class AuthorizationOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
            {
                operation.parameters = new List<Parameter>();
            }
            operation.parameters.Add(new Parameter()
            {
                name = "Authorization",
                @in = "header",
                description = "Access Token",
                required = false,
                type="String",
                @default= "bearer xLcKiJ34Tz9-TKJzKgG5hZyHJBdDQEjbTPjZXIDPF2zdZ_Jj84NV64WaWhsyFhdEU2I0XcqTgRQhSf-J8jTNbibvXaTvnRZsGwIiHXWWtVvJ2e3GKu4eeDTogS_uuzucuuD8NbF96zhZXGn-6IcmVVw5K0g8Ya5E-2_IhI0wIazAa7Orj8XUSkE2U44JJXE2ssSwB74ppAuP-MOBlTjP6WgKsAn4oTiQ4lvWLkVLsMSYuCbUEvSmh-csMEy2xjJy-HNTe4BUlEn8vbH_10AH5_FTCgLg8A0Xvn2wCwM5kaUQR4g00QG_DXWAMbEbbML_1irpSdT6RBcA_vUoLwM6kD9U2Roek3o-bEqx78CPHkCQBFfKrIYOEEQvyHXLvCIt43O3LEp2tChgoqw-oOOIDNBSyoWOiB-ZSOhNfddS6e9MRnyJwsXRR1izdJ-LijuCb_NwWg-7dYye2CKWGMQYG0-Xlu-xmU5LBbcw8DU0hJzwh1yyq640UNt7QH-AJlHT"
            });
        }
    }
}