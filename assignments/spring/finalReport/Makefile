FILE = final

all: writeup

writeup:
	latex $(FILE).tex
	bibtex bu1.aux
	bibtex bu2.aux
	bibtex bu3.aux
	latex $(FILE).tex
	latex $(FILE).tex
	dvips -R -Poutline -t letter $(FILE).dvi -o $(FILE).ps
	ps2pdf $(FILE).ps

clean:
	rm -f *.pdf *.ps *.dvi *.out *.log *.aux *.bbl *.blg *.pyg *.toc

.PHONY: all show clean ps pdf showps
