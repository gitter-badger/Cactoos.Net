﻿namespace Cactoos.Scalar
{
    public class ParsedInt : IScalar<int>
    {
        private IScalar<string> _source;

        public ParsedInt(IScalar<string> source)
        {
            _source = source;
        }

        public ParsedInt(string source) : this(new ValueScalar<string>(source))
        {

        }

        public int Value()
        {
            return int.Parse(_source.Value());
        }
    }
}
