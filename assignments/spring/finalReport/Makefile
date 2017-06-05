FILE = final

all: writeup

writeup:
	pdflatex --shell-escape ${FILE}.tex

clean:
	rm -f *.ps *.dvi *.out *.log *.aux *.bbl *.blg *.pyg *.toc

.PHONY: all show clean ps pdf showps
