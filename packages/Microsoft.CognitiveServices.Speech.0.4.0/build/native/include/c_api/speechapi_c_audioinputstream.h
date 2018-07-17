//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE.md file in the project root for full license information.
//
// speechapi_c_audioinputstream.h: Public API declarations for AudioInputStream related C methods
//

#pragma once
#include <speechapi_c_common.h>

/// <summary>
/// A structure that describes the audio format.
/// Currently, only PCM with 16-bit samples, 16 KHz sample rate, and a single channel (Mono) is supported.
/// </summary>
typedef struct _AudioInputStreamFormatC
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

// forward declaration
struct _AudioInputStream;

/// <summary>
/// Signature of a function being called to get the format of the audio stream.
/// </summary>
/// <param name="context">The pointer to the struct _AudioInputStream for which the parameter is queried.</param>
/// <param name="pformat">The pointer to the AudioInputStreamFormat buffer, or null if querying the size of the structure.</param>
/// <param name="cbFormat">The size of the AudioInputStreamFormat buffer being passed, or 0 if querying the size of the structure.</param>
/// <returns>The size of the AudioInputStreamFormat buffer required to hold the format information.</returns>
typedef int (*PAUDIOINPUTSTREAM_GETFORMAT_CALLBACK_FUNC)(struct _AudioInputStream *context, struct _AudioInputStreamFormatC* pformat, int cbFormat);

/// <summary>
/// Signature of a function being called to get the data from the audio stream.
/// </summary>
/// <param name="context">The pointer to the struct _AudioInputStream for which the data is queried.</param>
/// <param name="pbuffer">The pointer to the buffer to which to copy the audio data.</param>
/// <param name="cbBuffer">The size of the buffer.</param>
/// <returns>The number of bytes being copied to the buffer.</returns>
typedef int (*PAUDIOINPUTSTREAM_READ_CALLBACK_FUNC)(struct _AudioInputStream *context, unsigned char* pbuffer, int cbBuffer);

/// <summary>
/// Signature of a function being called to close the audio stream.
/// </summary>
/// <param name="context">The pointer to the struct _AudioInputStream to close.</param>
typedef void(*PAUDIOINPUTSTREAM_CLOSE_CALLBACK_FUNC)(struct _AudioInputStream *context);


/// <summary>
/// A structure with function pointers for the 3 operations defined on an AudioInputStream.
/// </summary>
typedef struct _AudioInputStream
{
    /// <summary>
    /// The Function being called to get the format of the audio data.
    /// </summary>
    PAUDIOINPUTSTREAM_GETFORMAT_CALLBACK_FUNC GetFormat;

    /// <summary>
    /// The Function being called to get the data of the audio stream.
    /// </summary>
    PAUDIOINPUTSTREAM_READ_CALLBACK_FUNC Read;

    /// <summary>
    /// The Function being called to close the audio stream.
    /// </summary>
    PAUDIOINPUTSTREAM_CLOSE_CALLBACK_FUNC Close;
} SpeechApi_AudioInputStream;
