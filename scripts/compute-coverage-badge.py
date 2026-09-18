"""Reads a Cobertura XML report and prints the Forget.Core package's line coverage as GitHub Actions
step outputs (pct, color), for use in the coverage-badge job in .github/workflows/build-test.yml.

Usage: python3 compute-coverage-badge.py <path-to-cobertura.xml>
"""

import sys
import xml.etree.ElementTree as ET
from decimal import Decimal, ROUND_HALF_UP

root = ET.parse(sys.argv[1]).getroot()
package = root.find(".//package[@name='Forget.Core']")

if package is None:
    sys.exit("Forget.Core package not found in coverage report")

pct = (Decimal(package.get("line-rate")) * 100).quantize(Decimal("0.1"), rounding=ROUND_HALF_UP)
color = "brightgreen" if pct >= 75 else "yellow" if pct >= 50 else "orange" if pct >= 25 else "red"

print(f"pct={pct}")
print(f"color={color}")
