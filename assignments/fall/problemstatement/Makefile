FILE = problemstatement

all: writeup

writeup:
	latex $(FILE).tex
	dvips -R -Poutline -t letter $(FILE).dvi -o $(FILE).ps
	ps2pdf $(FILE).ps

clean:
	rm -f *.pdf *.ps *.dvi *.out *.log *.aux *.bbl *.blg *.pyg

.PHONY: all show clean ps pdf showps
