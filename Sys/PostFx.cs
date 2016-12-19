using Torque3D;

namespace Game.Sys
{
    [ConsoleClass]
    public class PostFx : PostEffect
    {
        public static void Init()
        {
            // TODO no singletons
            Global.eval(@"singleton GFXStateBlockData( PFX_DefaultStateBlock )
{
   zDefined = true;
   zEnable = false;
   zWriteEnable = false;

   samplersDefined = true;
   samplerStates[0] = SamplerClampLinear;
};

singleton ShaderData( PFX_PassthruShader )
{
   DXVertexShaderFile 	= ""shaders/common/postFx/postFxV.hlsl"";
   DXPixelShaderFile 	= ""shaders/common/postFx/passthruP.hlsl"";

//   OGLVertexShaderFile  = ""shaders/common/postFx/gl//postFxV.glsl"";
//   OGLPixelShaderFile   = ""shaders/common/postFx/gl/passthruP.glsl"";

   samplerNames[0] = ""$inputTex"";

   pixVersion = 2.0;
};");
        }

        public void inspectVars()
        {
            string name = getName();
            string globals = $"${name}::*";
            //TODO inspectVars(globals);
        }

        public void viewDisassembly()
        {
            string file = dumpShaderDisassembly();

            if (string.IsNullOrEmpty(file))
            {
                Global.echo( "PostEffect::viewDisassembly - no shader disassembly found." );
            }
            else
            {
                Global.echo($"PostEffect::viewDisassembly - shader disassembly file dumped ( {file} ).");
                Global.openFile( file );
            }
        }

        // Return true if we really want the effect enabled.
        // By default this is the case.
        public bool onEnabled()
        {
            return true;
        }
    }
}