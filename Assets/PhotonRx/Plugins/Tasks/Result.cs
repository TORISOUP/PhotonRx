using System;

namespace PhotonRx
{
    /// <summary>
    /// 成功か失敗かどちらかの状態を表す
    /// </summary>
    /// <typeparam name="L">失敗時の型</typeparam>
    /// <typeparam name="R">成功時の型</typeparam>
    public interface IResult<L, R>
    {
        bool IsSuccess { get; }
        bool IsFailure { get; }

        Success<L, R> ToSuccess { get; }
        Failure<L, R> ToFailure { get; }

        IResult<L2, R2> Bind<L2, R2>(Func<L, IResult<L2, R2>> fl, Func<R, IResult<L2, R2>> fr);

        IResult<L, R> AsResult { get; }
    }

    public static class ResultExtensions
    {

        public static IResult<L, R2> Map<L, R, R2>(this IResult<L, R> self, Func<R, R2> f)
        {
            return self.Bind(Failure.Create<L, R2>, r => Success.Create<L, R2>(f(r)));
        }

        public static IResult<L, R2> FlatMap<L, R, R2>(
            this IResult<L, R> self,
            Func<R, IResult<L, R2>> f)
        {
            return self.Bind(Failure.Create<L, R2>, f);
        }
    }

    /// <summary>
    /// 成功を表す
    /// </summary>
    public struct Success<L, R> : IResult<L, R>
    {
        public R Value { get; private set; }
        public bool IsSuccess { get { return true; } }
        public bool IsFailure { get { return false; } }
        public Success<L, R> ToSuccess { get { return this; } }
        public Failure<L, R> ToFailure { get { throw new IllegalAccessToResultObjectException(); } }

        public IResult<L2, R2> Bind<L2, R2>(Func<L, IResult<L2, R2>> fl, Func<R, IResult<L2, R2>> fr)
        {
            return fr(Value);
        }

        public IResult<L, R> AsResult { get { return this; } }

        public Success(R success) : this()
        {
            Value = success;
        }

    }

    public static class Success
    {
        public static IResult<L, R> Create<L, R>(R success)
        {
            return new Success<L, R>(success);
        }
    }

    /// <summary>
    /// 失敗を表す
    /// </summary>
    public struct Failure<L, R> : IResult<L, R>
    {
        public L Value { get; private set; }
        public bool IsSuccess { get { return false; } }
        public bool IsFailure { get { return true; } }
        public Success<L, R> ToSuccess { get { throw new IllegalAccessToResultObjectException(); } }
        public Failure<L, R> ToFailure { get { return this; } }

        public IResult<L2, R2> Bind<L2, R2>(Func<L, IResult<L2, R2>> fl, Func<R, IResult<L2, R2>> fr)
        {
            return fl(Value);
        }

        public Failure(L failuer) : this()
        {
            Value = failuer;
        }

        public IResult<L, R> AsResult { get { return this; } }

    }

    public static class Failure
    {
        public static IResult<L, R> Create<L, R>(L failuer)
        {
            return new Failure<L, R>(failuer);
        }
    }

    public class IllegalAccessToResultObjectException : Exception
    {

    }
}