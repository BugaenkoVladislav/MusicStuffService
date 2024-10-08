// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: music.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace MusicStuffBackend {
  public static partial class Music
  {
    static readonly string __ServiceName = "Music";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::MusicStuffBackend.Track> __Marshaller_Track = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::MusicStuffBackend.Track.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::MusicStuffBackend.Result> __Marshaller_Result = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::MusicStuffBackend.Result.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::MusicStuffBackend.Id> __Marshaller_Id = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::MusicStuffBackend.Id.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::MusicStuffBackend.String> __Marshaller_String = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::MusicStuffBackend.String.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::MusicStuffBackend.Tracks> __Marshaller_Tracks = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::MusicStuffBackend.Tracks.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::MusicStuffBackend.Track, global::MusicStuffBackend.Result> __Method_AddNewTrack = new grpc::Method<global::MusicStuffBackend.Track, global::MusicStuffBackend.Result>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AddNewTrack",
        __Marshaller_Track,
        __Marshaller_Result);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::MusicStuffBackend.Id, global::MusicStuffBackend.Result> __Method_RemoveTrack = new grpc::Method<global::MusicStuffBackend.Id, global::MusicStuffBackend.Result>(
        grpc::MethodType.Unary,
        __ServiceName,
        "RemoveTrack",
        __Marshaller_Id,
        __Marshaller_Result);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::MusicStuffBackend.Id, global::MusicStuffBackend.Track> __Method_GetTrackById = new grpc::Method<global::MusicStuffBackend.Id, global::MusicStuffBackend.Track>(
        grpc::MethodType.Unary,
        __ServiceName,
        "GetTrackById",
        __Marshaller_Id,
        __Marshaller_Track);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::MusicStuffBackend.String, global::MusicStuffBackend.Tracks> __Method_FindTracksByName = new grpc::Method<global::MusicStuffBackend.String, global::MusicStuffBackend.Tracks>(
        grpc::MethodType.Unary,
        __ServiceName,
        "FindTracksByName",
        __Marshaller_String,
        __Marshaller_Tracks);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::MusicStuffBackend.String, global::MusicStuffBackend.Tracks> __Method_FindTracksByAuthor = new grpc::Method<global::MusicStuffBackend.String, global::MusicStuffBackend.Tracks>(
        grpc::MethodType.Unary,
        __ServiceName,
        "FindTracksByAuthor",
        __Marshaller_String,
        __Marshaller_Tracks);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::MusicStuffBackend.MusicReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of Music</summary>
    [grpc::BindServiceMethod(typeof(Music), "BindService")]
    public abstract partial class MusicBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::MusicStuffBackend.Result> AddNewTrack(global::MusicStuffBackend.Track request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::MusicStuffBackend.Result> RemoveTrack(global::MusicStuffBackend.Id request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::MusicStuffBackend.Track> GetTrackById(global::MusicStuffBackend.Id request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::MusicStuffBackend.Tracks> FindTracksByName(global::MusicStuffBackend.String request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::MusicStuffBackend.Tracks> FindTracksByAuthor(global::MusicStuffBackend.String request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(MusicBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_AddNewTrack, serviceImpl.AddNewTrack)
          .AddMethod(__Method_RemoveTrack, serviceImpl.RemoveTrack)
          .AddMethod(__Method_GetTrackById, serviceImpl.GetTrackById)
          .AddMethod(__Method_FindTracksByName, serviceImpl.FindTracksByName)
          .AddMethod(__Method_FindTracksByAuthor, serviceImpl.FindTracksByAuthor).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, MusicBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_AddNewTrack, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::MusicStuffBackend.Track, global::MusicStuffBackend.Result>(serviceImpl.AddNewTrack));
      serviceBinder.AddMethod(__Method_RemoveTrack, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::MusicStuffBackend.Id, global::MusicStuffBackend.Result>(serviceImpl.RemoveTrack));
      serviceBinder.AddMethod(__Method_GetTrackById, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::MusicStuffBackend.Id, global::MusicStuffBackend.Track>(serviceImpl.GetTrackById));
      serviceBinder.AddMethod(__Method_FindTracksByName, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::MusicStuffBackend.String, global::MusicStuffBackend.Tracks>(serviceImpl.FindTracksByName));
      serviceBinder.AddMethod(__Method_FindTracksByAuthor, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::MusicStuffBackend.String, global::MusicStuffBackend.Tracks>(serviceImpl.FindTracksByAuthor));
    }

  }
}
#endregion
