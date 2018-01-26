using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Ocelot.JWTAuthorizePolicy
{
    /// <summary>
    /// 必要参数类
    /// </summary>
    public class PermissionRequirement:IAuthorizationRequirement
    {
        /// <summary>
        /// 无权限action
        /// </summary>
        public string DeniedAction { get; set; }
        /// <summary>
        /// 认证授权类型
        /// </summary>
        public string ClaimType { get; set; }

        /// <summary>
        /// 请求路径
        /// </summary>
        public string LoginPath { get; set; } = "/Api/Login";
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 订阅人
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; }
        /// <summary>
        /// 签名验证
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="deniedAction">拒绝请求的url</param>
        /// <param name="claimType">声明类型</param>
        /// <param name="issuer">发行人</param>
        /// <param name="audience">订阅人</param>
        /// <param name="signingCredentials">签名验证实体</param>
        /// <param name="expiration">过期时间</param>
        public PermissionRequirement(string deniedAction, string claimType, string issuer, string audience,
            SigningCredentials signingCredentials, TimeSpan expiration)
        {
            DeniedAction = deniedAction;
            ClaimType = claimType;
            Issuer = issuer;
            Audience = audience;
            SigningCredentials = signingCredentials;
            Expiration = expiration;
        }
    }
}
