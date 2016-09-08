﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Vault.Endpoints.Sys
{
    public class MountInfo
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public MountConfig Config { get; set; }
    }

    public class MountConfig
    {
        public string DefaultLeaseTtl { get; set; }
        public string MaxLeaseTtl { get; set; }
    }

    public partial class SysEndpoint
    {
        public Task<Dictionary<string, MountInfo>> ListMounts()
        {
            return ListMounts(CancellationToken.None);
        }

        public Task<Dictionary<string, MountInfo>> ListMounts(CancellationToken ct)
        {
            return _client.Get<Dictionary<string, MountInfo>>($"{UriPathBase}/mounts", ct);
        }

        public Task Mount(string path, MountInfo mountInfo)
        {
            return Mount(path, mountInfo, CancellationToken.None);
        }

        public Task Mount(string path, MountInfo mountInfo, CancellationToken ct)
        {
            return _client.PutVoid($"{UriPathBase}/mounts/{path}", mountInfo, ct);
        }

        public Task Unmount(string path)
        {
            return Unmount(path, CancellationToken.None);
        }

        public Task Unmount(string path, CancellationToken ct)
        {
            return _client.Delete($"{UriPathBase}/mounts/{path}", ct);
        }

        public Task Remount(string from, string to)
        {
            return Remount(from, to, CancellationToken.None);
        }

        public Task Remount(string from, string to, CancellationToken ct)
        {
            var request = new RemountRequest
            {
                From = from,
                To = to
            };
            return _client.PutVoid($"{UriPathBase}/remount", request, ct);
        }

        public Task TuneMount(string path, MountConfig mountConfig)
        {
            return TuneMount(path, mountConfig, CancellationToken.None);
        }

        public Task TuneMount(string path, MountConfig mountConfig, CancellationToken ct)
        {
            return _client.PostVoid($"{UriPathBase}/mounts/{path}/tune", mountConfig, ct);
        }

        public Task<MountConfig> MountConfig(string path)
        {
            return MountConfig(path, CancellationToken.None);
        }

        public Task<MountConfig> MountConfig(string path, CancellationToken ct)
        {
            return _client.Get<MountConfig>($"{UriPathBase}/mounts/{path}/tune", ct);
        }

        internal class RemountRequest
        {
            public string From { get; set; }
            public string To { get; set; }
        }
    }
}
