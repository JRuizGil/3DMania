Shader "Unlit/StencilShader" // Nombre del shader, visible en el menú de selección de shaders
{
    Properties
    {
        // Define una propiedad entera de tipo rango entre 0 y 255 para usar como ID del stencil buffer
        [IntRange] _StencilID ("Stencil ID", Range(0,255)) = 0 
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque" // Indica que el objeto es opaco (no transparente)
            "RenderPipeline" = "UniversalPipeline" // Especifica que este shader es para el Universal Render Pipeline (URP)
            "Queue" = "Geometry" // Define la cola de renderizado; 'Geometry' es para objetos opacos normales
        }

        Pass
        {
            // Configura la mezcla de colores: Zero One significa que no se escribe color (totalmente transparente)
            Blend Zero One 
            
            // No escribe en el Z-buffer (profundidad)
            ZWrite Off 

            // Configura la operación del Stencil Buffer
            Stencil
            {
                ref [_StencilID] // Valor de referencia que se usará en las pruebas de stencil
                Comp Always // Siempre pasa la prueba de stencil
                Pass Replace // Si pasa la prueba, reemplaza el valor del stencil con el valor de referencia
                Fail Keep // Si falla la prueba de stencil (aunque con 'Always' nunca fallará), se mantiene el valor actual
            }
        }
    }
}

