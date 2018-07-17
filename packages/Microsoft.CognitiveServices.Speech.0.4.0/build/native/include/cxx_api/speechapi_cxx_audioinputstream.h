//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//
// speechapi_cxx_audioinputstream.h: Public API declarations for AudioInputStream C++ base class
//

#pragma once
#include <string>
#include <speechapi_cxx_common.h>


namespace Microsoft {
namespace CognitiveServices {
namespace Speech {

/// <summary>
/// A structure that describes the audio format.
/// Currently, only PCM with 16-bit samples, 16 KHz sample rate, and a single channel (Mono) is supported.
/// </summary>
typedef struct _AudioInputStreamFormatCPP
{
    /// <summary>
    /// The format of the audio, valid values: 1 (PCM)
    /// </summary>
    int32_t         FormatTag;

    /// <summary>
    /// The number of channels, valid values: 1 (Mono).
    /// </summary>
    int32_t         Channels;

    /// <summary>
    /// The sample rate, valid values: 16000.
    /// </summary>
    int32_t         SamplesPerSec;

    /// <summary>
    /// Average bytes per second, usually calculated as nSamplesPerSec * nChannels * ceil(wBitsPerSample, 8).
    /// </summary>
    int32_t         AvgBytesPerSec;

    /// <summary>
    /// The size of a single frame, valid values: nChannels * ceil(wBitsPerSample, 8).
    /// </summary>
    int32_t         BlockAlign;

    /// <summary>
    /// The bits per sample, valid values: 16
    /// </summary>
    int32_t         BitsPerSample;

} AudioInputStreamFormat, *PAudioInputStreamFormat;


/// <summary>
/// An interface that defines an audio input stream.
/// </summary>
/// <remarks>
/// Derive from this class and implement its function to provide your own
/// data as an Audio input stream.
/// <remarks>
class AudioInputStream
{
public:
    /// <summary>
    /// Destructor, does nothing.
    /// </summary>
    virtual ~AudioInputStream() {}

    /// <summary>
    /// This function is called to get the format of the audio stream.
    /// </summary>
    /// <param name="pformat">The pointer to the AudioInputStreamFormat buffer, or null if querying the size of the structure.</param>
    /// <param name="cbFormat">The size of the AudioInputStreamFormat buffer being passed, or 0 if querying the size of the structure.</param>
    /// <returns>The size of the AudioInputStreamFormat buffer required to hold the format information.</returns>
    virtual size_t GetFormat(AudioInputStreamFormat* pformat, size_t cbFormat) = 0;

    /// <summary>
    /// This function is called to get data from the audio stream.
    /// </summary>
    /// <param name="dataBuffer">The pointer to the buffer to which to copy the audio data.</param>
    /// <param name="size">The size of the buffer.</param>
    /// <returns>The number of bytes being copied to the buffer.</returns>
    virtual size_t Read(char* dataBuffer, size_t size) = 0;

    /// <summary>
    /// This function is called to close the audio stream.
    /// </summary>
    virtual void Close() = 0;

protected:

    /*! \cond PROTECTED */

    /// <summary>
    /// Constructor, does nothing.
    /// </summary>
    AudioInputStream() {};

    /*! \endcond */

private:
    DISABLE_COPY_AND_MOVE(AudioInputStream);
};


} } } // Microsoft::CognitiveServices::Speech
