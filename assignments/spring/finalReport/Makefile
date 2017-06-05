FILE = final

all: writeup

writeup:
	pdflatex --shell-escape ${FILE}.tex

clean:
	rm -rf *.ps *.dvi *.out *.log *.aux *.bbl *.blg *.pyg *.toc ./_minted-final

.PHONY: all show clean ps pdf showps
