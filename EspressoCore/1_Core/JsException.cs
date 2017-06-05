//MIT, 2015-2017, WinterDev, EngineKit, brezza92

// This file is part of the VroomJs library.
//
// Author:
//     Federico Di Gregorio <fog@initd.org>
//
// Copyright © 2013 Federico Di Gregorio <fog@initd.org>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.



using System;
namespace Espresso
{

    public class JsException : Exception
    {

        internal static JsException Create(JsConvert convert, JsError error)
        {
            throw new NotSupportedException();
            //string type = (string)convert.FromJsValue(ref error.Type);
            //string resource = (string)convert.FromJsValue(ref error.Resource);
            //string message = (string)convert.FromJsValue(ref error.Message);
            //int line = error.Line;
            //int column = error.Column + 1; // because zero based.
            //JsObject nativeException = (JsObject)convert.FromJsValue(ref error.Exception);

            //JsException exception;
            //if (type == "SyntaxError")
            //{
            //    exception = new JsSyntaxError(type, resource, message, line, column);
            //}
            //else
            //{
            //    exception = new JsException(type, resource, message, line, column, nativeException);
            //}
            //return exception;
        }

        public JsException()
        {
        }

        public JsException(string message)
            : base(message)
        {
        }

        public JsException(string message, Exception inner)
            : base(message, inner)
        {

        }

        //protected JsException(SerializationInfo info, StreamingContext context)
        //    : base(info, context)
        //{
        //}

        internal JsException(string type, string resource, string message, int line, int col, JsObject error)
            : base(string.Format("{0}: {1} at line: {2} column: {3}.", resource, message, line, col))
        {
            _type = type;
            _resource = resource;
            _line = line;
            _column = col;
            _nativeException = error;
        }

        // Native V8 exception objects are wrapped by special instances of JsException.

        public JsException(JsObject nativeException)
        {
            _nativeException = nativeException;
        }

        readonly JsObject _nativeException;

        public JsObject NativeException
        {
            get { return _nativeException; }
        }


        protected string _type;
        public string Type { get { return _type; } }

        protected string _resource;
        public string Resource { get { return _resource; } }

        protected int _line;
        public int Line { get { return _line; } }

        protected int _column;
        public int Column { get { return _column; } }
    }

    public class JsSyntaxError : JsException
    {
        internal JsSyntaxError(string type, string resource, string message, int line, int col)
            : base(type, resource, message, line, col, null)
        {
        }
    }
}
