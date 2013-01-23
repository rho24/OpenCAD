using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace OpenCAD.OpenGL.Shaders
{
    public abstract class BaseShaderProgram : IShaderProgram
    {
        public int Handle { get; private set; }


        private List<string> _logs = new List<string>();

        public BaseShaderProgram()
        {
            Handle = GL.CreateProgram();

        }

        public void CompileShader(ShaderType type, string source)
        {
            source = PreProcess(source);
            int shaderHandle = GL.CreateShader(type);
            GL.ShaderSource(shaderHandle, source);
            GL.CompileShader(shaderHandle);
            var log = GL.GetShaderInfoLog(shaderHandle);
            int compileResult;
            GL.GetShader(shaderHandle, ShaderParameter.CompileStatus, out compileResult);
            if (compileResult != 1)
            {
                _logs.Add(log);
                Console.WriteLine(log);
                Console.WriteLine("Compile Error:" + type);
            }
            GL.AttachShader(Handle, shaderHandle);
        }

        public string PreProcess(string source)
        {

            return source;
        }

        public void Bind()
        {
            GL.UseProgram(Handle);
        }

        public void UnBind()
        {
            GL.UseProgram(0);
        }

        public void Link()
        {
            GL.LinkProgram(Handle);
            int linkResult;
            GL.GetProgram(Handle, ProgramParameter.LinkStatus, out linkResult);

            if (linkResult != 1)
            {
                string info;
                GL.GetProgramInfoLog(Handle, out info);
                Console.WriteLine(info);
                _logs.Add(info);
                throw new Exception(String.Join("\n--------\n", _logs.ToArray()));
            }
            Console.WriteLine("linked");
            ErrorCode err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at Shader: " + err);
        }

    }
}