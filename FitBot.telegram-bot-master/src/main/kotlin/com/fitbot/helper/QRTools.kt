package com.fitbot.helper

import io.github.g0dkar.qrcode.QRCode
import java.nio.file.FileSystems
import java.nio.file.Path
import java.util.UUID
import javax.imageio.ImageIO

object QRTools {
    private const val FILE_FORMAT = "png"
    private val DEFAULT_VERSION: QRSize = QRSize.SMALL

    private fun encodeText(text: String?, size: Int): String {
        val imageData = QRCode(text!!).render(cellSize = size)
        val path: Path =
            FileSystems.getDefault().getPath(java.lang.String.format("images/%s.%s", UUID.randomUUID(), FILE_FORMAT))
        ImageIO.write(imageData, FILE_FORMAT, path.toFile())
        return path.toAbsolutePath().toString()
    }

    fun encodeText(text: String?, QRSIze: QRSize = DEFAULT_VERSION): String {
        val size = when (QRSIze) {
            QRSize.SMALL -> 32
            QRSize.MEDIUM -> 64
            QRSize.LARGE -> 128
        }
        return encodeText(text, size)
    }
}
