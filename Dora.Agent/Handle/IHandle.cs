using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dora.Agent.Handle
{
    public interface IHandle
    {
        /// <summary>
        /// 发送数据到Server
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task SendAsync(Context context);

        /// <summary>
        /// 从Server中接收数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task ReceiveAsync(Context context);
    }
}
