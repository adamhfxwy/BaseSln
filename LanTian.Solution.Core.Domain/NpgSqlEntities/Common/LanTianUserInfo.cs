

using LanTian.Solution.Core.Domain.NpgSqlEntities.DeviceMaintain;

namespace LanTian.Solution.Core.Domain.NpgSqlEntities.Common
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class LanTianUserInfo : BaseEntity
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string Name { get; private set; } = null!;
        /// <summary>
        /// 联系方式
        /// </summary>
        public string Cellphone { get; private set; } = null!;
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; private set; } = null!;
        private LanTianUserInfo()
        {

        }
        public LanTianUserInfo(string name, string cellphone, string address)
        {
            this.Name = name;
            this.Cellphone = cellphone;
            this.Address = address;
        }
        public void ChangeName(string name)
        {
            Name = name;
        }
        public void ChangeCellphone(string cellphone)
        {
            this.Cellphone = cellphone;
        }
        public void ChangeAddress(string address)
        {
            this.Address = address;
        }
    }
}
