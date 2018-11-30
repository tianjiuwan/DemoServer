// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace AppMain
{
    using System;
    using System.IO;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;
    using DotNetty.Codecs;
    using DotNetty.Codecs.Protobuf;
    using DotNetty.Handlers.Logging;
    using DotNetty.Handlers.Tls;
    using DotNetty.Transport.Bootstrapping;
    using DotNetty.Transport.Channels;
    using DotNetty.Transport.Channels.Sockets;
    using DotNetty.Transport.Libuv;

    class Program
    {
        static async Task RunServerAsync()
        {
            IEventLoopGroup bossGroup;
            IEventLoopGroup workerGroup;

            bossGroup = new MultithreadEventLoopGroup(1);
            workerGroup = new MultithreadEventLoopGroup();    

            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workerGroup);
                bootstrap.Channel<TcpServerSocketChannel>();
                bootstrap
                    .Option(ChannelOption.SoBacklog, 100)
                    .Handler(new LoggingHandler("SRV-LSTN"))
                    .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        //if (tlsCertificate != null)
                        //{
                        //    pipeline.AddLast("tls", TlsHandler.Server(tlsCertificate));
                        //}
                        pipeline.AddLast(new LoggingHandler("SRV-CONN"));
                        pipeline.AddLast("stringDecoder", new PBDecoder());
                        //pipeline.AddLast("stringEncoder", new ProtobufVarint32LengthFieldPrepender());
                        //pipeline.AddLast("echo", new EchoServerHandler());
                        pipeline.AddLast(new TestServerHandler());
                    }));
                IChannel boundChannel = await bootstrap.BindAsync(8007);
                Console.ReadLine();
                await boundChannel.CloseAsync();
            }
            finally
            {
                await Task.WhenAll(
                    bossGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)),
                    workerGroup.ShutdownGracefullyAsync(TimeSpan.FromMilliseconds(100), TimeSpan.FromSeconds(1)));
            }
        }

        static void Main() => RunServerAsync().Wait();
    }
}