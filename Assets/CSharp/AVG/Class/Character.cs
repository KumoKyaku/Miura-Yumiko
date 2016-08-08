
namespace AVG
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Character
    {

        /// <summary>
        /// 角色ID
        /// </summary>
        public int ID { get;private set; }

        /// <summary>
        /// 角色名字
        /// </summary>
        public string Name { get;private set; }

        public Character(int iD)
        {
            this.ID = iD;
        }
    }
}
