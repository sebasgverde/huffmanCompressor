This is a compressor for text files based in Huffman codes and binary trees. Given a text, it creates a tree based in the symbol frequencies. This way, the more frequent is a symbol, the less bits it needs to represent it. When it compresses a file, a dictionary with the pair symbol-code is created to decode (decompress). Also 2 versions of a compressed file, a .txt with the binary string and a binary .dat which is the actual compressed file.


# Demo

[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/jkEBqMtMb2M/0.jpg)](https://www.youtube.com/watch?v=jkEBqMtMb2M)

# Use
The tool is a console app, it has 2 functions with their respective command:

- Compress:  This command will create 3 files in <path to output> folder: kesyFile.txt, compressedFile.txt and compressedFile.dat

    ```bash
    huffmanCompressor.exe 0 <path to text file> <path to output>
    ```

    
- Decompress: This will create one file called decompressedFile.txt in <path to output> folder

    ```bash
    huffmanCompressor.exe 1 <path to .dat and keys> <path to output>
    ```
    
	
# Spacial complexity Analysis

As we can see in source code, both programs are merged in the main and are separated by an if statement. Therefore, we will start for each one from the try-catch blocks that read the txt and we will take only elements with size bigger than 1.

## Huffman codification
Ordenar()
Arreglo tamaño n
ordenar = n
/*****************************/
### buscarPosicion()
arreglo tamaño n
árbol tamaño m

buscarPosicion() = n +m
/******************************/
### recorridoArbol()
2 listas tamaño n
árbol tamaño m

recorridoArbol() = 2n + m
/********************************/
### codificacionHuffman()
lista tamaño n
buscarPosicion() = n + m
recorridoArbol() = 2n + m

codificacionHuffman() = 3n + 2m
/*******************************************/
### Comprimir()
Lista tamaño n
Texto tamaño n
Texto tamaño m

Comprimir() = 2n + m
/*******************************************/
### contarFrec()
texto tamaño n
lista tamaño m

contarFrec() =  n + m

/***************************************/


### Main()
Texto tamaño n
2 Listas tamaño m
contarFrec() =  n + m
codificacionHuffman() = 2n + m
Comprimir() = n + m

Main() = n + 2m +n +m +2n +m +n+m = 5n + 5m


## Huffman decodification

### decodificacionHuffman()
texto tamaño n
texto tamaño m
lista tamaño m

decodificacionHuffman() = n + 2m

/******************************/
### Main()
Lista tamaño n
Texto tamaño m
Texto tamaño n
decodificacionHuffman() = n +2m

Main() =2n +m +n +2m = 3n + 3m


# Temporal Complexity analysis

As we can see in source code, both programs are merged in the main and are separated by an if statement. Therefore, we will start for each one from the try-catch blocks that read the .txt and in the cases where there are loops with flags we will take only the worse of the cases.

## Huffman codification

### Ordenar()
Para i = 1 a n
Para j = 1 a n
	c
Fin para
fin para
ordenar = n2
/*****************************/
### buscarPosicion()
c
mientras 1 a n
	c
fin mientras
buscarPosicion() = n
/******************************/
### recorridoArbol()
c
mientras  1 a n
	mientras 1 a n
		c
	fin mientras
	c
fin mientras

recorridoArbol() = n
/********************************/
### codificacionHuffman()
ordenar = n2
mientras 1 a n
	c
	buscarPosicion() = n
fin mientras
recorridoArbol() = n

codificacionHuffman() = n2 + n2 +n = 2 n2
/*******************************************/
### Comprimir()
C
Para 1 a n
	Para 1 a n
		C
	Fin para
Fin para

Comprimir() = n2
/*******************************************/
### contarFrec()
c
mientras 1 a n
	para 1 a n
		c
	fin para
	c
fin mientras
c

contarFrec() =  n2

/***************************************/


### Main()
Try
Mientras 1 a n
		c
Fin mientras
Fin try

contarFrec() =  n2
codificacionHuffman() = 2 n2

Try
Mientras 1 a n
		c
Fin mientras
Fin try

Comprimir() = n2

Main() = n + n2 + 2n2 + n + n2  = 4 n2


## Huffman decodification

### decodificacionHuffman()
c
para 1 a n
c
para 1 a n
	c
fin para
c
fin para
c
mientras 1 a n
	c
fin mientras
c

decodificacionHuffman() = n2 + n = n2

/******************************/
### Main()
Try
Mientras 1 a n
		c
Fin mientras
Fin try
C
Try
Mientras 1 a n
		c
Fin mientras
Fin try
decodificacionHuffman() = n2 

Main() = n + n + n2 = n2

## Complexity Summary

### codificaciónHuffman spacial complexity is O(n)  = 5n + 5m
### decodificacionHuffman spacial complexity is O(n) =  3n + 3m

### codificaciónHuffman Temporal Complexity is T(n)  = 4 n2
### decodificacionHuffman Temporal Complexity is T(n) = n2 










