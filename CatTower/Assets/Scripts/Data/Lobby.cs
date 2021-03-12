using System.Collections.Generic;

namespace CatTower
{
    public class UserListResponse : BaseResponse
    {
        public List<UserInfo> userList;
        public string roomId;
        public UserInfo host;
    }

    public class ExitUserRequest
    {
        public UserInfo userInfo;
        public string roomId;
    }

    public class ReadyByHostRequest
    {
        public UserInfo hostInfo;
        public string roomId;
    }

    public class StartGameResponse
    {
        public string roomId;
    }
}