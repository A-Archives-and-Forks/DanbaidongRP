# ReSTIR Global Illumination

This implementation intentionally follows the older DanbaidongRP prototype supplied with the project. The first integration target is behavioral familiarity; visibility-aware and unbiased variants can be layered on later without changing the render-pipeline integration.

## Pass mapping

1. `ReSTIRGlobalIllumination.raytrace` (`SingleRayGen`) generates cosine-weighted one-bounce candidates and performs initial reservoir sampling.
2. `ReSTIRGlobalIllumination.compute` reprojects the previous reservoir, validates depth and normals, applies the optional temporal Jacobian, performs biased spatial reuse, copies the final reservoir into camera history, and resolves indirect diffuse.
3. `ReSTIRGlobalIlluminationDenoiser.compute` performs history reprojection with neighborhood clamping and luminance moments, followed by the old three-step edge-aware spatial filter.
4. `ReSTIRGlobalIlluminationPass.cs` allocates double-buffered camera histories and records the complete RenderGraph sequence before deferred lighting.

## Reservoir estimator

Initial directions use the spatiotemporal blue-noise cosine hemisphere distribution. For a Lambertian receiver, `f * cos(theta) / pdf` reduces to the surface albedo, so the reservoir target is the luminance of outgoing radiance at the selected secondary point. The stored `avgWeight` is the old implementation's normalized reservoir weight:

`avgWeight = (weightSum / M) / target(selectedSample)`

Temporal and spatial reuse transport the selected secondary point to a new receiver. The optional temporal correction and the spatial pass use the old geometry Jacobian and clamp it to 10.

## Ray payload

The indirect payload stores `t`, radiance, recursion/sample bookkeeping, pixel coordinates, and a two-component octahedral hit normal. It does not store a ray cone or hit position. The ray-generation shader reconstructs the position from `Origin + Direction * t`.

Temporal reuse, spatial reuse, and resolve are compute passes and therefore do not add a visibility payload to the indirect DXR pipeline. This avoids the D3D12 pipeline-state error caused by mixing the 24-byte visibility payload with the former 72-byte indirect payload in one ray-tracing shader.

## Current limits

- Deferred rendering and hardware ray tracing are required.
- One indirect bounce is generated.
- Reservoirs and denoising run at full resolution.
- Spatial reuse is the biased form used by the old prototype.
- Reused samples are not re-tested with a visibility ray during resolve.
- Environment misses use the sky fallback from the old ray-generation shader.
- Reservoir history uses three `R32G32B32A32_UInt` textures.

These are deliberate compatibility limits for the first port and are the natural extension points for later work.
